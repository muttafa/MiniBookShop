using System.ComponentModel.DataAnnotations;

public class CreditCard
{
    [Key]
    public int CreditCardID { get; set; }
    public int UserID { get; set; }
    public string KartName { get; set; }
    public string Expiration { get; set; }
    public string CVV { get; set; }
    public string CardNo { get; set; }
}