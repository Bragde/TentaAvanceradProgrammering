using Microsoft.AspNetCore.Mvc;
using OrderService.Data.Entities;
using OrderService.Models;
using OrderService.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderService.Controllers
{
    [ApiController]
    [Route("OrderService/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderDto order)
        {
            if (order == null)
                return BadRequest("Providet object has null value.");
            var orderEntity = new Order(order);

            try
            {                
                var orderProductsEntities = order.OrderedProducts.Select(x => new OrderProduct(x)).ToList();
                _orderRepository.CreateOrder(orderEntity, orderProductsEntities);
            }
            catch (Exception)
            {
                return BadRequest("Provided object is not in correct format.");
            }
            
            if (!await _orderRepository.Save())
                return BadRequest("Save item to shoppingcart failed.");

            return CreatedAtRoute("GetOrder",
                new { orderId = orderEntity.OrderId },
                new OrderDto(orderEntity));
        }

        [HttpGet("{orderId}", Name = "GetOrder")]
        /* Get order is not implemented. Method exists only so 
         * CreatedAtRoute from CreateOrder will work*/
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
