using System;
using CBA.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CBA.CORE.Models;
//using CBA.DATA;

namespace CBA.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }
        //public DbSet ApplicationUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasData(
                   new Customer
                   {
                       Id = 1,
                       FirstName = "James",
                       LastName = "Bond",
                       Gender = Core.Enums.Gender.any,
                   }
            );
        }

    }
}
