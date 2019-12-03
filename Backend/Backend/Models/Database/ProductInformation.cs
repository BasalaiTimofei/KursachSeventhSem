namespace Backend.Models.Database
{
    public class ProductInformation
    {
        public string Id { get; set; }

        //Info

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}