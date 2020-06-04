using CatalogService.Data.Entities;
using System;

namespace CatalogService.Models
{
    public class CatalogItemDto
    {
        public CatalogItemDto(){}

        public CatalogItemDto(CatalogItem itemEntity)
        {
            Id = itemEntity.Id;
            Title = itemEntity.Title;
            Price = itemEntity.Price;
            ImageUrl = itemEntity.ImageUrl;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
