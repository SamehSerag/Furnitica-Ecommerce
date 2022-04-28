﻿using AngularProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularProject.Data
{
    public class ShoppingDb : DbContext
    {

        public ShoppingDb(DbContextOptions options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            //builder.Entity<OrderProducts>()
            //   .HasOne(Tc => Tc.Order)
            //   .WithMany(c => c.OrderProducts)
            //   .HasForeignKey(bc => bc.OrderId);
            //base.OnModelCreating(builder);

            //builder.Entity<OrderProducts>()
            //   .HasOne(Tc => Tc.Product)
            //   .WithMany(c => c.OrderProducts)
            //   .HasForeignKey(bc => bc.ProductId);
            //base.OnModelCreating(builder);
        }



        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
