namespace Backend.Models.View
{
    public class ProductCreate
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ProviderId { get; set; }

        public string[] UrlImages { get; set; }
        public ProductInfo ProductInformation { get; set; }
    }
}