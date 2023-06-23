using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMarket.Application.CustomValidator.Abstract;
using TMarket.Application.ServiceModels;
using TMarket.Application.Services.Abstract;
using TMarket.Persistence.DbModels;
using TMarket.Persistence.UnitOfWork;

namespace TMarket.Application.Services.Concrete
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidationDictionary _validationDictionary;
        private readonly IConfiguration _config;

        public CartService(IUnitOfWork unitOfWork, IValidationDictionary validationDictionary,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _validationDictionary = validationDictionary;
            _config = configuration;
        }

        public IEnumerable<CartDTO> GetAllWithNoTracking()
        {
            var items = _unitOfWork.CartRepository.GetAll(p => p,
                p => !p.IsDeleted,
                p => p.OrderBy(x => x.Id),
                source => source
                .Include(o => o.CartProducts)
                .ThenInclude(o => o.Product),
                true);
            return items;
        }

        public async Task<bool> InsertOrderAsync(CartServiceModel cart)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var products = await _unitOfWork.ProductRepository.GetAllAsyncWithTracking();
                var users = await _unitOfWork.UserRepository.GetAllAsyncWithNoTracking();

                var user = users.FirstOrDefault(x => x.Id == cart.UserId);

                if (user == null)
                {
                    _validationDictionary.AddError("UserId", "შეყვანილი იუზერი არ არსებობს!");
                    return false;
                }

                var doesUserHaveCart = DoesUserHaveActiveCart(user.Id);
                CartDTO newCart;


                if (doesUserHaveCart == null)
                {
                    newCart = new CartDTO { UserId = cart.UserId, };
                    await _unitOfWork.CartRepository.InsertAsync(newCart);
                }
                else
                {
                    newCart = doesUserHaveCart;
                }


                if (!await AddOrderProduct(cart, products.ToList(), newCart))
                {
                    await _unitOfWork.RollbackAsync();
                    return false;
                }

                await _unitOfWork.SaveChangesAsync();

                if (doesUserHaveCart == null)
                {
                    var jobId = BackgroundJob.Schedule(() => DeleteCart(newCart.Id),
                        TimeSpan.FromMinutes(_config.GetValue<int>("CartOptions:CartExpireTimeInMinute")));
                    newCart.JobId = jobId;
                    await _unitOfWork.SaveChangesAsync();
                }

                await _unitOfWork.CommitAsync();

                return true;

            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        protected async Task<bool> AddOrderProduct(CartServiceModel cart, List<ProductDTO> products, CartDTO cartDTO)
        {
            List<int> productIds = cart.CartProducts.Select(x => x.ProductId).ToList();

            for (int i = 0; i < productIds.Count; i++)
            {
                var productId = productIds[i];
                var product = products.FirstOrDefault(x => x.Id == productId && x.IsAvailable);
                var cartQuantity = cart.CartProducts.Single(x => x.ProductId == productId).Quantity;

                if (!IsValidProduct(product, cartQuantity, productId, i))
                {
                    continue;
                }

                product.AvailableCount -= cartQuantity;

                if (product.AvailableCount == 0)
                {
                    product.IsAvailable = false;
                }

                var cartProductInCart = DoesCartHaveProduct(cartDTO, productId);
                if (cartProductInCart != null)
                {
                    cartProductInCart.Quantity += cartQuantity;
                    await _unitOfWork.CartProductRepository.UpdateAsync(cartProductInCart);
                }
                else
                {
                    await _unitOfWork.CartProductRepository.InsertAsync(new CartProductDTO { CartId = cartDTO.Id, ProductId = productId, Quantity = cartQuantity });
                }
            }


            return _validationDictionary.IsValid;
        }

        protected bool IsValidProduct(ProductDTO product, int orderQuantity, int productId, int orderNumber)
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

        public async Task DeleteCart(int id)
        {
            if (!IsCartDeleted(id))
            {
                await _unitOfWork.CartRepository.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private bool IsCartDeleted(int id)
        {
            var carts = GetAllWithNoTracking();
            return !carts.Any(cart => cart.Id == id);
        }

        private CartDTO DoesUserHaveActiveCart(int userId)
        {
            var carts = GetAllWithNoTracking();

            return carts.FirstOrDefault(x => x.UserId == userId);
        }

        private CartProductDTO DoesCartHaveProduct(CartDTO cartDTO, int productId)
        {
            return cartDTO.CartProducts.FirstOrDefault(cartProduct => cartProduct.ProductId == productId);
        }
    }
}
