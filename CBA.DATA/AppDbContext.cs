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
                     //FullName = "James Bond",
                     //Address = "Bermuda Triangle",
                     //ConsumerInfo = "smart",
                     //ID = 1,
                     //CustomerLongID = "64545566"
                     //Email = "Jamesbond007@gmail.com",
                     //Gender = Core.Enums.Gender.Male,
                     //PhoneNumber = "007007007007",
                     FirstName = "James Bond",
                     LastName = "King",
                     Gender = Core.Enums.Gender.Male,
                     Id = 1,
                     
                   }
            );
        }

    }
}
