using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCartService.Models;
using ShoppingCartService.Repositories;

namespace ShoppingCartService.Controllers
{
    [ApiController]
    [Route("ShoppingCartService/[controller]/[action]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        }

        [HttpGet("{shoppingCartId}")]
        public ActionResult<IEnumerable<ShoppingCartItemDto>> GetShoppingCartItemsByShoppingCartId(Guid shoppingCartId)
        {
            var items = _shoppingCartRepository.GetShoppingCartItemsByShoppingCartId(shoppingCartId);

            if (items == null)
                return NotFound();

            var itemsDto = items.Select(x => new ShoppingCartItemDto(x)).ToList();

            return Ok(itemsDto);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCartItemDto>> AddItemToShoppingCart(ShoppingCartItemDto shoppingCartItemDto)
        {
            var shoppingCartItemEntity = new ShoppingCartItem(shoppingCartItemDto);

            var itemInDb = _shoppingCartRepository.GetItemFromShoppingCart(shoppingCartItemEntity);
            if (itemInDb == null)
            {
                shoppingCartItemEntity.Amount = 1;
                _shoppingCartRepository.AddItemToShoppingCart(shoppingCartItemEntity);
            }
            else
                itemInDb.Amount++;

            if (!await _shoppingCartRepository.Save())
                return BadRequest("Add item to shoppingcart failed.");

            return Ok();
        }
    }
}
