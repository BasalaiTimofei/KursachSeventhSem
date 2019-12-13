using Backend.Interfaces;

namespace Backend.Models.Database
{
    public class OrderProduct //: IEntity
    {
        //public string Id { get; set; }

        public string OrderId { get; set; }
        public Order Order { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}