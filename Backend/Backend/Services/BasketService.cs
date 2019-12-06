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
    public class BasketService
    {

        private readonly ApplicationContext _applicationContext;
        private readonly AssessmentService _assessmentService;

        public BasketService(ApplicationContext applicationContext, AssessmentService assessmentService)
        {
            _applicationContext = applicationContext;
            _assessmentService = assessmentService;
        }

        public async Task Add(string userId, string[] productId)
        {
            var products = productId.Select(product => new BasketProductDatabaseModel
            {
                BasketId = _applicationContext.Baskets.FirstOrDefaultAsync(w => w.UserId == userId).Result.Id,
                ProductId = product
            }).ToList();
            await _applicationContext.BasketProducts.AddRangeAsync(products);

            await _applicationContext.SaveChangesAsync();
        }

        public async Task Delete(string userId, string[] productId)
        {
            foreach (var product in productId)
            {
                _applicationContext.BasketProducts.RemoveRange(
                    _applicationContext.BasketProducts.Where(
                        w => w.ProductId == product && w.Basket.UserId == userId));
            }

            await _applicationContext.SaveChangesAsync();
        }

        public async Task<List<ProductSmallViewModel>> Get(string userId)
        {
            var productsDb = _applicationContext.Baskets.FindAsync(userId).Result.Products;
            var productView = new List<ProductSmallViewModel>();
            foreach (var product in productsDb)
            {
                productView.Add(new ProductSmallViewModel
                {
                    Id = product.ProductId,
                    Name = product.Product.Name,
                    Assessment = await _assessmentService.AverageValue(product.ProductId),
                    ScreenSize = product.Product.ProductInformation.ScreenSize,
                    Price = Math.Round(product.Product.Price, 1).ToString(CultureInfo.InvariantCulture),
                    OderCount = product.Product.Orders.Count,
                    ProviderName = product.Product.Provider.Name,
                    UrlImage = product.Product.UrlImages.FirstOrDefault()
                });
            }

            return productView;
        }
    }
}
