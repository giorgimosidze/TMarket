using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TMarket.Application.ServiceModels;
using TMarket.WEB.RequestModels.Orders;
using TMarket.Application.Services.Abstract;

namespace TMarket.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        // GET: api/Orders
        [HttpGet]
        public ActionResult<IEnumerable<OrderResponse>> GetOrders()
        {
            var orders = _orderService.GetAllAsyncWithNoTracking();

            return _mapper.Map<List<OrderResponse>>(orders);
        }


        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult> PostOrder(OrderRequest order)
        {
            if (await _orderService.InsertOrderAsync(_mapper.Map<OrderServiceModel>(order)))
            {
                return Ok("შეკვეთა წარმატებით დაემატა!");
            }

            
           return BadRequest(ModelState.Where(ms => ms.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToArray()));
        }
    }
}
