using Backend.Interfaces;

namespace Backend.Models.Database
{
    public class BasketProduct //: IEntity
    {
        //public string Id { get; set; }

        public string BasketId { get; set; }
        public Basket Basket { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}