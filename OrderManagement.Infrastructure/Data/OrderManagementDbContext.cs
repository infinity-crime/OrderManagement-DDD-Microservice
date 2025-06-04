using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Data
{
    public class OrderManagementDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public OrderManagementDbContext(DbContextOptions<OrderManagementDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(x =>
            {
                x.HasKey(o => o.Id);

                x.Property(o => o.Country)
                .IsRequired()
                .HasMaxLength(50);

                x.Property(o => o.City)
                .IsRequired()
                .HasMaxLength(50);

                x.Property(o => o.Street)
                .IsRequired()
                .HasMaxLength(100);

                x.Property(o => o.Postcode)
                .IsRequired();

                x.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18, 2)")
                .HasDefaultValue(0m);

                // Cascading deletion
                x.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(x =>
            {
                x.HasKey(oi => oi.Id);

                x.Property(oi => oi.Id)
                .ValueGeneratedOnAdd();

                x.Property(oi => oi.ProductId)
                .IsRequired();

                x.Property(oi => oi.Quantity)
                .IsRequired();

                x.Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

                // Disable creating OrderItem without navigating to Order
                x.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
