using Microsoft.AspNetCore.Mvc;
using ShoppingCartService.Models;
using ShoppingCartService.Repositories;
using System;

namespace ShoppingCartService.Controllers
{
    [ApiController]
    [Route("shoppingcartservice/shoppingcarts/{shoppingCartId}/shoppingCartItems")]
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartItemController(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        }

        [HttpGet("{shoppingCartItemId}", Name = "GetItemByShoppingCartItemId")]
        public IActionResult GetItemByShoppingCartItemId(Guid shoppingCartItemId)
        {
            var item = _shoppingCartRepository.GetItemByShoppingCartItemId(shoppingCartItemId);
            if (item == null)
                return NotFound();

            return Ok(new ShoppingCartItemDto(item));
        }

        [HttpPost]
        public ActionResult<ShoppingCartItemDto> AddItemToShoppingCart(ShoppingCartItemDto shoppingCartItemDto)
        {
            var shoppingCartItemEntity = new ShoppingCartItem(shoppingCartItemDto);

            _shoppingCartRepository.AddItemToShoppingCart(shoppingCartItemEntity);

            var itemToReturn = new ShoppingCartItemDto(shoppingCartItemEntity);
            
            return CreatedAtRoute(
                "GetItemByShoppingCartItemId",
                new {
                    shoppingCartId = shoppingCartItemEntity.ShoppingCartId,
                    shoppingCartItemId = shoppingCartItemEntity.ShoppingCartItemId },
                itemToReturn);
        }
    }
}
