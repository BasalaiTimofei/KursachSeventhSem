using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Backend.Context;
using Backend.Models.Database;
using Backend.Models.View;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class ProductService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly AssessmentService _assessmentService;

        public ProductService(ApplicationContext applicationContext, AssessmentService assessmentService)
        {
            _applicationContext = applicationContext;
            _assessmentService = assessmentService;
        }

        public async Task<string> Create(ProductCreateViewModel model)
        {
            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                DateTimeCreate = DateTime.Now,
                Description = model.Description,
                Price = model.Price,
                UrlImage = model.UrlImage,
                ProviderId = model.ProviderId
            };
            await _applicationContext.Products.AddAsync(product);

            var productInformation = new ProductInformation
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
            _applicationContext.Assessments.RemoveRange(_applicationContext.Products.FindAsync(productId).Result
                .Assessments);
            _applicationContext.OrderProducts.RemoveRange(_applicationContext.Products.FindAsync(productId).Result
                .Orders);
            _applicationContext.BasketProducts.RemoveRange(_applicationContext.Products.FindAsync(productId).Result
                .Baskets);
            _applicationContext.Comments.RemoveRange(_applicationContext.Products.FindAsync(productId).Result
                .Comments);
            _applicationContext.Products.Remove(_applicationContext.Products.FindAsync(productId).Result);
            await _applicationContext.SaveChangesAsync();
        }



        public void DeleteRange(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                _applicationContext.ProductInformations.Remove(
                    _applicationContext.ProductInformations.Single(w => w.ProductId == product.Id));
                _applicationContext.Assessments.RemoveRange(_applicationContext.Products.FindAsync(product.Id)
                    .Result.Assessments);
                _applicationContext.OrderProducts.RemoveRange(_applicationContext.Products.FindAsync(product.Id)
                    .Result.Orders);
                _applicationContext.BasketProducts.RemoveRange(_applicationContext.Products.FindAsync(product.Id)
                    .Result.Baskets);
                _applicationContext.Comments.RemoveRange(_applicationContext.Products.FindAsync(product.Id).Result
                    .Comments);
                _applicationContext.Products.Remove(_applicationContext.Products.FindAsync(product.Id).Result);
            }

        }

        public async Task<List<ProductSmallViewModel>> GetAll()
        {
            var productsDb = _applicationContext.Products;
            var productView = new List<ProductSmallViewModel>();
            foreach (var product in productsDb)
            {
                productView.Add(new ProductSmallViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Assessment = Math.Round(_applicationContext.Assessments.Where(w => w.ProductId == product.Id)
                        .Average(s => s.Value), 1).ToString(CultureInfo.InvariantCulture),
                    ScreenSize = "", //product.ProductInformation.ScreenSize,
                    Price = Math.Round(product.Price, 1).ToString(CultureInfo.InvariantCulture),
                    OderCount = 3, //product.Orders.Count,
                    ProviderName = "", //product.Provider.Name,
                    UrlImage = product.UrlImage
                });
            }

            return productView;
        }


        public async Task<ProductFullViewModel> GetById(string productId)
        {
            var productDb = await _applicationContext.Products.FindAsync(productId);
            var productView = new ProductFullViewModel
            {
                Id = productId,
                Name = productDb.Name,
                Price = Math.Round(productDb.Price, 1).ToString(CultureInfo.InvariantCulture),
                Description = productDb.Description,
                ProductInformation = new ProductInformationViewModel
                {
                    Memory = productDb.ProductInformation.Memory,
                    ScreenSize = productDb.ProductInformation.ScreenSize,
                    Ram = productDb.ProductInformation.Ram,
                    OperatingSystem = productDb.ProductInformation.OperatingSystem,
                    NumberOfCores = productDb.ProductInformation.NumberOfCores
                },
                UrlImage = productDb.UrlImage,
                ProviderName = productDb.Provider.Name
            };

            return productView;
        }

        public async Task<List<ProductSmallViewModel>> GetByProperty(PropertyViewModel model)
        {
            var productView = new List<ProductSmallViewModel>();
            var productDb = new List<Product>();
            if (model.MinPrice == 0 && model.MaxPrice == 0 && model.Providers != null)
            {
                foreach (var provider in model.Providers)
                {
                    productDb.AddRange(_applicationContext.Products.Where(w => w.ProviderId == provider.Id));
                }

                foreach (var product in productDb)
                {
                    productView.Add(new ProductSmallViewModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Assessment = await _assessmentService.AverageValue(product.Id),
                        ScreenSize = product.ProductInformation.ScreenSize,
                        Price = Math.Round(product.Price, 1).ToString(CultureInfo.InvariantCulture),
                        OderCount = product.Orders.Count,
                        ProviderName = product.Provider.Name,
                        UrlImage = product.UrlImage
                    });
                }

                return productView;
            }

            if (model.Providers == null)
            {
                productDb.AddRange(_applicationContext.Products.Where(s => Convert.ToInt32(s.Price) >= model.MinPrice ||
                                                                           Convert.ToInt32(s.Price) <= model.MaxPrice));

                foreach (var product in productDb)
                {
                    productView.Add(new ProductSmallViewModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Assessment = await _assessmentService.AverageValue(product.Id),
                        ScreenSize = product.ProductInformation.ScreenSize,
                        Price = Math.Round(product.Price, 1).ToString(CultureInfo.InvariantCulture),
                        OderCount = product.Orders.Count,
                        ProviderName = product.Provider.Name,
                        UrlImage = product.UrlImage
                    });
                }

                return productView;
            }

            foreach (var provider in model.Providers)
            {
                productDb.AddRange(_applicationContext.Products
                    .Where(w => w.ProviderId == provider.Id)
                    .Where(s => Convert.ToInt32(s.Price) >= model.MinPrice ||
                                Convert.ToInt32(s.Price) <= model.MaxPrice));
            }

            foreach (var product in productDb)
            {
                productView.Add(new ProductSmallViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Assessment = await _assessmentService.AverageValue(product.Id),
                    ScreenSize = product.ProductInformation.ScreenSize,
                    Price = Math.Round(product.Price, 1).ToString(CultureInfo.InvariantCulture),
                    OderCount = product.Orders.Count,
                    ProviderName = product.Provider.Name,
                    UrlImage = product.UrlImage
                });
            }

            return productView;
        }
    }
}
