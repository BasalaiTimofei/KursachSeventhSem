namespace Backend.Models.Database
{
    public class BasketProductDatabaseModel
    {
        public string BasketId { get; set; }
        public BasketDatabaseModel Basket { get; set; }

        public string ProductId { get; set; }
        public ProductDatabaseModel Product { get; set; }
    }
}