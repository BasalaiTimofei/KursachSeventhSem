using System.Collections.Generic;

namespace Backend.Models.Database
{
    public sealed class BasketDatabaseModel
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public UserDatabaseModel User { get; set; }

        public List<BasketProductDatabaseModel> Products { get; set; }

        public BasketDatabaseModel()
        {
            Products = new List<BasketProductDatabaseModel>();
        }
    }
}