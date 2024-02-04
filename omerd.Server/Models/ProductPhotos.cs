using System.ComponentModel.DataAnnotations;

namespace omerd.Server.Models
{
    public class ProductPhotos
    {
        [Key]
        public int ProductPhotoID { get; set; }
        public string ProductPhotoUrl { get; set; }
        public int ProductID { get; set; }

        public Products Product { get; set; }
    }
}
