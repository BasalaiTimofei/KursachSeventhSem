using System.Collections.Generic;

namespace Backend.Models.Database
{
    public class ProviderDatabaseModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<ProductDatabaseModel> Products { get; set; }

        public ProviderDatabaseModel()
        {
            Products = new List<ProductDatabaseModel>();
        }
    }
}