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

        [HttpGet("{orderId}", Name = "GetOrder")]
        public IActionResult GetOrder(Guid OrderId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderDto order)
        {
            var orderEntity = new Order(order);
            var orderProductsEntities = order.OrderedProducts.Select(x => new OrderProduct(x)).ToList();
            _orderRepository.CreateOrder(orderEntity, orderProductsEntities);
            
            if (!await _orderRepository.Save())
                return BadRequest("Add item to shoppingcart failed.");

            return CreatedAtRoute("GetOrder",
                new { orderId = orderEntity.OrderId },
                new OrderDto(orderEntity));
        }
    }
}
