using AutoMapper;
using ECommerceApi.Products.Db;
using ECommerceApi.Products.Profiles;
using ECommerceApi.Products.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace ECommmerce.Api.Products.Test
{
    public class ProductsServiceTest
    {
        [Fact]
        public async void GetProductsReturnsAllProduct()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProduct))
                .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);
            var productprofile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productprofile));
            var mapper = new Mapper(configuration);
            var ProductProvider = new ProductsProviders(dbContext, null, mapper);

            var product = await ProductProvider.GetProductAsync();

            Assert.True(product.IsSucess);
            Assert.True(product.products.Any());
            Assert.Null(product.ErrorMessage);
        }


        [Fact]
        public async void GetProductsValidProductID()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsValidProductID))
                .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);
            var productprofile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productprofile));
            var mapper = new Mapper(configuration);
            var ProductProvider = new ProductsProviders(dbContext, null, mapper);

            var product = await ProductProvider.GetProductAsync(1);

            Assert.True(product.IsSucess);
            Assert.NotNull(product.product);
            Assert.True(product.product.Id == 1);
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async void GetProductsInValidProductID()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsInValidProductID))
                .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);
            var productprofile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productprofile));
            var mapper = new Mapper(configuration);
            var ProductProvider = new ProductsProviders(dbContext, null, mapper);

            var product = await ProductProvider.GetProductAsync(-1);

            Assert.False(product.IsSucess);
            Assert.Null(product.product);
            Assert.NotNull(product.ErrorMessage);
        }

        private void CreateProducts(ProductDbContext dbContext)
        {
            for (int i = 1; i < 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                });
            }
            dbContext.SaveChanges();
        }
    }
}
