using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using TMarket.Application.ServiceModels;
using TMarket.Application.Services.Abstract;
using TMarket.WEB.RequestModels.Cart;
using TMarket.WEB.RequestModels.Orders;

namespace TMarket.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper, IOrderService orderService)
        {
            _orderService = orderService;
            _cartService = cartService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetProductsInCart()
        {
            var carts = _cartService.GetAllWithNoTracking();

            return Ok(_mapper.Map<List<CartResponse>>(carts));
        }

        [HttpPost]
        public async Task<ActionResult> PostCart(CartRequest cart)
        {
            if (await _cartService.InsertOrderAsync(_mapper.Map<CartServiceModel>(cart)))
            {
                return Ok("კალათაში შეკვეთა წარმატებით დაემატა!");
            }

            return BadRequest(ModelState.Where(ms => ms.Value.Errors.Count > 0)
                     .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToArray()));
        }
        [HttpPost("{id}")]
        public async Task<ActionResult> PostCartById(int id)
        {
            var carts = _cartService.GetAllWithNoTracking();
            var cart = carts.SingleOrDefault(x => x.Id == id);

            if(cart == null)
            {
                return BadRequest("კალათა ვერ მოიძებნა");
            }

            List<ProductOrderRequest> orderProducts = new List<ProductOrderRequest>();
            foreach (var cartProduct in cart.CartProducts)
            {
                orderProducts.Add(new ProductOrderRequest { ProductId = cartProduct.ProductId, Quantity = cartProduct.Quantity });
            }
            var order = new OrderRequest { UserId = cart.UserId, OrderProducts = orderProducts };

            if (await _orderService.InsertOrderAsync(_mapper.Map<OrderServiceModel>(order)))
            {
                BackgroundJob.Delete(cart.JobId);
                await _cartService.DeleteCart(cart.Id);
                return Ok("შეკვეთა წარმატებით დაემატა!");
            }

            return BadRequest(ModelState.Where(ms => ms.Value.Errors.Count > 0)
                     .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToArray()));
        }

    }
}