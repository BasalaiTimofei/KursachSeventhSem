using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Context;
using Backend.Models.Database;

namespace Backend.Services
{
    public class BasketService
    {
        private readonly ApplicationContext _applicationContext;

        public BasketService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task Create(string userId)
        {
            var basket = new Basket
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId
            };

            await _applicationContext.Baskets.AddAsync(basket);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task Add(string userId, string productId)
        {
            _applicationContext.BasketProducts.Add(new BasketProduct
            {
                BasketId = _applicationContext.Baskets.FirstOrDefault(w => w.UserId == userId)?.Id, ProductId = productId
            });

            await _applicationContext.SaveChangesAsync();
        }

        //public async Task Delete(string[] basketId)
        //{
        //    foreach (var item in basketId)
        //    {
                
        //    }
        //}
    }
}