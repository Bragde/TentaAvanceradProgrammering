using OrderService.Data.Entities;
using System;
using System.Collections.Generic;

namespace OrderService.Models
{
    public class OrderDto
    {
        public OrderDto()
        {

        }
        public OrderDto(Order orderEntity)
        {
            OrderId = orderEntity.OrderId;
            UserId = orderEntity.UserId;
            OrderPlaced = orderEntity.OrderPlaced;
            OrderTotal = orderEntity.OrderTotal;
        }

        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderPlaced { get; set; }
        public decimal OrderTotal { get; set; }
        public IEnumerable<OrderProductDto> OrderedProducts { get; set; }

    }
}
