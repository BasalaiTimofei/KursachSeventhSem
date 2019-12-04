namespace Backend.Models.Database
{
    public class ProductInformation
    {
        public string Id { get; set; }

        public string OperatingSystem { get; set; }
        public float ScreenSize { get; set; }
        public int Ram { get; set; }
        public int Memory { get; set; }
        /// <summary>
        /// Количесто ядер
        /// </summary>
        public int NumberOfCores { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}