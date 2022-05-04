using System;
using CBA.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using CBA.DATA;

namespace CBA.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //   
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasData(
                   new Customer
                   {
                       Id = 1,
                       FirstName = "Super",
                       LastName = "User",
                       Gender = Core.Enums.Gender.any,
                       Role = Core.Enums.Roles.SuperAdmin,
                   }
            );
        }

    }
}
