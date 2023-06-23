using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TMarket.Persistence.DbModels;
using TMarket.Persistence.UnitOfWork;
using TMarket.Application.Services.Abstract;
using TMarket.Application.CustomValidator.Abstract;
using TMarket.Application.ServiceModels;

namespace TMarket.Application.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidationDictionary _validationDictionary;

        public OrderService(IUnitOfWork unitOfWork,
            IValidationDictionary validationDictionary)
        {
            _unitOfWork = unitOfWork;
            _validationDictionary = validationDictionary;
        }

        public IEnumerable<OrderDTO> GetAllAsyncWithNoTracking()
        {
            var items = _unitOfWork.OrderRepository.GetAll(p => p,
                p => !p.IsDeleted,
                p => p.OrderBy(x => x.Id),
                source => source
                .Include(o => o.OrderProducts)
                .ThenInclude(o => o.Product),
                true);
            return items;
        }

        public async Task<bool> InsertOrderAsync(OrderServiceModel order)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var products = await _unitOfWork.ProductRepository.GetAllAsyncWithTracking();
                var users = await _unitOfWork.UserRepository.GetAllAsyncWithNoTracking();

                var user = users.FirstOrDefault(x => x.Id == order.UserId);

                if (user == null)
                {
                    _validationDictionary.AddError("UserId", "შეყვანილი იუზერი არ არსებობს!");
                    return false;
                }

                var newOrder = new OrderDTO { UserId = order.UserId, };
                await _unitOfWork.OrderRepository.InsertAsync(newOrder);

                if (!await AddOrderProduct(order, products.ToList(), newOrder.Id))
                {
                    await _unitOfWork.RollbackAsync();
                    return false;
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return true;

            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        protected async Task<bool> AddOrderProduct(OrderServiceModel order, List<ProductDTO> products, int orderId)
        {
            var productIds = order.OrderProducts.Select(x => x.ProductId).ToList();

            for (var i = 0; i < productIds.Count; i++)
            {
                var productId = productIds[i];
                var product = products.FirstOrDefault(x => x.Id == productId && x.IsAvailable);
                var orderQuantity = order.OrderProducts.Single(x => x.ProductId == productId).Quantity;

                if (!IsValidProduct(product, orderQuantity, i))
                {
                    continue;
                }

                product.AvailableCount -= orderQuantity;

                if (product.AvailableCount == 0)
                {
                    product.IsAvailable = false;
                }

                await _unitOfWork.OrderProductRepository.InsertAsync(new OrderProductDTO { OrderId = orderId, ProductId = productId, Quantity = orderQuantity });
            }


            return _validationDictionary.IsValid;
        }

        protected bool IsValidProduct(ProductDTO product, int orderQuantity, int orderNumber)
        {
            if (product == null)
            {
                _validationDictionary.AddError($"OrderProduct[{orderNumber}].ProductId", $"შეყვანილი პროდუქტი არ არსებობს!");
            }

            if (product?.AvailableCount < orderQuantity)
            {
                _validationDictionary.AddError($"OrderProduct[{orderNumber}].Quantity", $"შეკვეთილი პროდუქტის რაოდენობა ({orderQuantity}) აღემატება არსებულ მარაგს ({product?.AvailableCount})");
            }

            return _validationDictionary.IsValid;
        }
    }
}
