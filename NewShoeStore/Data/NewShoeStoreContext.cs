using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewShoeStore.Models;

namespace NewShoeStore.Data
{
    public class NewShoeStoreContext : DbContext
    {
        public NewShoeStoreContext (DbContextOptions<NewShoeStoreContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderShoe>()
                .HasKey(pr => new { pr.IdShose, pr.IdOrder });

            modelBuilder.Entity<OrderShoe>()
                .HasOne(pt => pt.Shoe)
                .WithMany(p => p.Orders)
                .HasForeignKey(pt => pt.IdShose);
            modelBuilder.Entity<OrderShoe>()
                .HasOne(pt => pt.Order)
                .WithMany(t => t.Shoes)
                .HasForeignKey(pt => pt.IdOrder);
        }


        public DbSet<NewShoeStore.Models.Category> Category { get; set; }

        public DbSet<NewShoeStore.Models.Customer> Customer { get; set; }

        public DbSet<NewShoeStore.Models.Order> Order { get; set; }

        public DbSet<NewShoeStore.Models.Shoe> Shoe { get; set; }

        public DbSet<NewShoeStore.Models.OrderShoe> OrderShoe { get; set; }
    }
}
