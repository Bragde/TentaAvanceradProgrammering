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
    [Route("shoppingcartservice/shoppingcarts")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        }

        [HttpGet("{shoppingCartId}")]
        public ActionResult<IEnumerable<ShoppingCartItemDto>> GetShoppingCartByShoppingCartId(Guid shoppingCartId)
        {
            var items = _shoppingCartRepository.GetShoppingCartByShoppingCartId(shoppingCartId);

            if (items == null)
                return NotFound();

            var itemsDto = items.Select(x => new ShoppingCartItemDto(x)).ToList();

            return Ok(itemsDto);
        }


    }
}
