namespace omerd.Server.ViewModels
{
    public class CartViewModel
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
    }


    public class RemoveItem
    {
        public int productID { get; set; }
        public int userID { get; set; }
    }
}
