using System.Collections.Generic;

namespace Backend.Models.Database
{
    public sealed class Basket
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public List<BasketProduct> Products { get; set; }

        public Basket()
        {
            Products = new List<BasketProduct>();
        }
    }
}