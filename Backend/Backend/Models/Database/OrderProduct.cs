namespace Backend.Models.Database
{
    public class OrderProductDatabaseModel
    {
        public string OrderId { get; set; }
        public OrderDatabaseModel Order { get; set; }

        public string ProductId { get; set; }
        public ProductDatabaseModel Product { get; set; }
    }
}