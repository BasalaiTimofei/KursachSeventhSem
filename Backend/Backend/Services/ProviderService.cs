using System;
using System.Threading.Tasks;
using Backend.Context;
using Backend.Models.Database;

namespace Backend.Services
{
    public class ProviderService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ProductService _productService;

        public ProviderService(ApplicationContext applicationContext, ProductService productService)
        {
            _applicationContext = applicationContext;
            _productService = productService;
        }

        public async Task Create(string providerName)
        {
            var provider = new Provider
            {
                Id = Guid.NewGuid().ToString(),
                Name = providerName
            };
            await _applicationContext.Providers.AddAsync(provider);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task Delete(string providerId)
        {
            _productService.DeleteRange(_applicationContext.Providers.FindAsync(providerId).Result.Products);
            _applicationContext.Providers.Remove(_applicationContext.Providers.FindAsync(providerId).Result);
            await _applicationContext.SaveChangesAsync();
        }
    }
}