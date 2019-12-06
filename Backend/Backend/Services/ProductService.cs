using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Context;
using Backend.Models.Database;
using Backend.Models.View;

namespace Backend.Services
{
    public class ProductService
    {
        private readonly ApplicationContext _applicationContext;

        public ProductService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<string> Create(ProductCreateViewModel model)
        {
            var product = new ProductDatabaseModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                DateTimeCreate = DateTime.Now,
                Description = model.Description,
                Price = model.Price,
                UrlImages = model.UrlImages,
                ProviderId = model.ProviderId
            };
            await _applicationContext.Products.AddAsync(product);

            var productInformation = new ProductInformationDatabaseModel
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = product.Id,
                Memory = model.ProductInformation.Memory,
                NumberOfCores = model.ProductInformation.NumberOfCores,
                OperatingSystem = model.ProductInformation.OperatingSystem,
                Ram = model.ProductInformation.Ram,
                ScreenSize = model.ProductInformation.ScreenSize
            };
            await _applicationContext.ProductInformations.AddAsync(productInformation);
            await _applicationContext.SaveChangesAsync();

            return product.Id;
        }

        public async Task Delete(string productId)
        {
            _applicationContext.ProductInformations.Remove(
                _applicationContext.ProductInformations.Single(w => w.ProductId == productId));
            _applicationContext.Assessments.RemoveRange(_applicationContext.Products.FindAsync(productId).Result.Assessment);
            _applicationContext.OrderProducts.RemoveRange(_applicationContext.Products.FindAsync(productId).Result.Orders);
            _applicationContext.BasketProducts.RemoveRange(_applicationContext.Products.FindAsync(productId).Result.Baskets);
            _applicationContext.Comments.RemoveRange(_applicationContext.Products.FindAsync(productId).Result.Comments);
            _applicationContext.Products.Remove(_applicationContext.Products.FindAsync(productId).Result);
            await _applicationContext.SaveChangesAsync();
        }

        public void DeleteRange(IEnumerable<ProductDatabaseModel> products)
        {
            foreach (var product in products)
            {
                _applicationContext.ProductInformations.Remove(
                    _applicationContext.ProductInformations.Single(w => w.ProductId == product.Id));
                _applicationContext.Assessments.RemoveRange(_applicationContext.Products.FindAsync(product.Id).Result.Assessment);
                _applicationContext.OrderProducts.RemoveRange(_applicationContext.Products.FindAsync(product.Id).Result.Orders);
                _applicationContext.BasketProducts.RemoveRange(_applicationContext.Products.FindAsync(product.Id).Result.Baskets);
                _applicationContext.Comments.RemoveRange(_applicationContext.Products.FindAsync(product.Id).Result.Comments);
                _applicationContext.Products.Remove(_applicationContext.Products.FindAsync(product.Id).Result);
            }
        }
    }
}
