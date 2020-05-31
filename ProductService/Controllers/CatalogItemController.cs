using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogService.Data.Entities;
using CatalogService.Models;
using CatalogService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("catalogservice/catalogitems")]
    public class CatalogItemController : ControllerBase
    {
        private readonly ICatalogRepository _catalogRepository;

        public CatalogItemController(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CatalogItemDto>> GetAll()
        {
            var items = _catalogRepository.GettAll();

            if (items == null)
                return NotFound();

            var itemsDto = items.Select(x => new CatalogItemDto(x)).ToList();

            return Ok(itemsDto);
        }

        [HttpGet("{catalogItemId}")]
        public ActionResult<CatalogItemDto> GetById(Guid catalogItemId)
        {
            var item =  _catalogRepository.GetById(catalogItemId);

            if (item == null)
                return NotFound();

            var itemDto = new CatalogItemDto(item);

            return Ok(itemDto);
        }
    }
}
