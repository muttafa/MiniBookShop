using System.ComponentModel.DataAnnotations;


namespace omerd.Server.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public decimal Star { get; set; }
        public List<ProductPhotos> ProductPhotos { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
