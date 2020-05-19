using CatalogService.Data.Entities;
using System;
using System.Collections.Generic;

namespace CatalogService.Repositories
{
    public interface ICatalogRepository
    {
        List<CatalogItem> GettAll();
        CatalogItem GetById(Guid id);
    }
}
