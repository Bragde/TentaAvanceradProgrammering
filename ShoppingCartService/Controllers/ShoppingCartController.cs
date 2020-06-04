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

        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<ShoppingCartItemDto>> GetShoppingCartItemsByUserId(string userId)
        {
            var items = _shoppingCartRepository.GetShoppingCartItemsByUserId(userId);

            if (items.Count == 0)
                return NotFound("No shoppingcart items were found.");

            var itemsDto = items.Select(x => new ShoppingCartItemDto(x)).ToList();

            return Ok(itemsDto);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCartItemDto>> AddItemToShoppingCart(ShoppingCartItemDto shoppingCartItemDto)
        {
            if (shoppingCartItemDto.CatalogItemId == null
                || shoppingCartItemDto.UserId == null)
                return BadRequest("Provided object does not contain necessary information.");

            var shoppingCartItemEntity = new ShoppingCartItem(shoppingCartItemDto);

            // If item already exists in users shoppingcart increase amount with one, else add one new item.
            var itemInDb = _shoppingCartRepository.GetItemFromShoppingCart(shoppingCartItemEntity);
            if (itemInDb == null)
            {
                shoppingCartItemEntity.Amount = 1;
                _shoppingCartRepository.AddItemToShoppingCart(shoppingCartItemEntity);
            }
            else
                itemInDb.Amount++;
           
            if (!await _shoppingCartRepository.Save())
                return BadRequest("Save item to shoppingcart failed.");

            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteShoppingCart(string userId)
        {
            _shoppingCartRepository.DeleteShoppingCart(userId);

            if (!await _shoppingCartRepository.Save())
                return BadRequest("Delete shoppingcart failed.");

            return Ok();
        }
    }
}
