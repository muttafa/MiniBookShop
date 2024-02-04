using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using omerd.Server.Models;
public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }


    public DbSet<Products> Products { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<ProductPhotos> ProductPhotos { get; set; }
    public DbSet<CartModel> CartModel { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<CreditCard> CreditCard { get; set; }

}

