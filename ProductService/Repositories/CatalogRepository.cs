using CatalogService.Data.Context;
using CatalogService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatalogService.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly CatalogDbContext _context;

        public CatalogRepository(CatalogDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public CatalogItem GetById(Guid id)
        {
            var item = _context.CatalogItems
                .Where(i => i.Id == id)
                .FirstOrDefault();

            return item;
        }

        public List<CatalogItem> GettAll()
        {
            var movies = _context.CatalogItems
                .OrderBy(i => i.Title)
                .ToList();

            return movies;
        }
    }
}
