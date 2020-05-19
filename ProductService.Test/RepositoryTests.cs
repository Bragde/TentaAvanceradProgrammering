using CatalogService.Data.Context;
using CatalogService.Data.Entities;
using CatalogService.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace CatalogService.Test
{
    public class RepositoryTest
    {
        [Fact]
        public void GetById()
        {
            using (var context = new CatalogDbContext())
            {
                var repository = new CatalogRepository(context);

                var sut = repository.GetById(new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"));

                Assert.Equal("90d6da79-e0e2-4ba8-bf61-2d94d90df801", sut.Id.ToString());
            }
        }

        [Fact]
        public void GetAll()
        {
            using (var context = new CatalogDbContext())
            {
                var repository = new CatalogRepository(context);

                var sut = repository.GettAll();

                Assert.Equal(4, sut.Count);
            }
        }
    }
}
