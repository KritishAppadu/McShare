using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McShare.Model
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Customer>().HasKey(r => new { r.CustomerID });

        //    _ = modelBuilder.Entity<Customer>().HasData(
        //                   new Customer() { CustomerID = "1", CustomerName = "Jean Luc", DOB = new DateTime(1987, 5, 2), CustomerType = "Regular", NumShares = 3, SharesPrice = 6000, Balance = 18000 },
        //                   new Customer() { CustomerID = "2", CustomerName = "Jean Loder", DOB = new DateTime(1999, 4, 6), CustomerType = "Once", NumShares = 2, SharesPrice = 6300, Balance = 12600 },
        //                   new Customer() { CustomerID = "3", CustomerName = "Todn Luc", DOB = new DateTime(1988, 5, 12), CustomerType = "Regular", NumShares = 5, SharesPrice = 1000, Balance = 5000 }
        //               );
        //}
    }
}
