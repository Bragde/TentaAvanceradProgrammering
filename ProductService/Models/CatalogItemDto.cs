using System;

namespace CatalogService.Models
{
    public class CatalogItemDto
    {
        public CatalogItemDto(){}

        public CatalogItemDto(Guid id, string title, decimal price, string imageUrl)
        {
            Id = id;
            Title = title;
            Price = price;
            ImageUrl = imageUrl;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
