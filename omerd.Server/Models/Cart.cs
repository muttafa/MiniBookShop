using System.ComponentModel.DataAnnotations;

namespace omerd.Server.Models
{
    public class CartModel
    {
        [Key]
        public int CartId { get; set; }
        public int UserId { get; set; } 
        public int ProductID { get; set; } 
        public int Count { get; set; } 
        public DateTime AddDate { get; set; }
        public byte isActive { get; set; }
    }
}
