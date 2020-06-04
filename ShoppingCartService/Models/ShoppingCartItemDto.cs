using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartService.Models
{
    public class ShoppingCartItemDto
    {
        public ShoppingCartItemDto(){}

        public ShoppingCartItemDto(ShoppingCartItem itemEntity)
        {
            ShoppingCartItemId = itemEntity.ShoppingCartItemId;
            CatalogItemId = itemEntity.CatalogItemId;
            Amount = itemEntity.Amount;
            UserId = itemEntity.UserId;
        }

        public Guid ShoppingCartItemId { get; set; }
        public Guid CatalogItemId { get; set; }
        public int Amount { get; set; }
        public string UserId { get; set; }
    }
}
