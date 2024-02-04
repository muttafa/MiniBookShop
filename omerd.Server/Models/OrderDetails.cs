using System.ComponentModel.DataAnnotations;

namespace omerd.Server.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Orders Order { get; set; }
        public Products Product { get; set; }
    }
}
