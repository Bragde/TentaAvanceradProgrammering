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
    [Route("catalogservice/catalogitem")]
    public class CatalogItemController : ControllerBase
    {
        private readonly ICatalogRepository _catalogRepository;

        public CatalogItemController(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CatalogItemDto>> GetAll()
        {
            var items = _catalogRepository.GettAll();

            var itemsDto = items.Select(x => new CatalogItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl
                })
                .ToList();

            return Ok(itemsDto);
        }
    }
}
