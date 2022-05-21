using System;
using CBA.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CBA.CORE.Models;
using System.Linq;
//using CBA.DATA;

namespace CBA.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AccountTypeManagement> AccountTypeManagements { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<GLAccount> GLAccounts { get; set; }
        public DbSet<TillAccount> TillAccounts { get; set; }
        public DbSet<GLCategory> GLCategories { get; set; }
        public DbSet<FineNames> FineNames { get; set; }
        public DbSet<TellerTill> TellerTills { get; set; }
        public DbSet<TellerPosting> TellerPostings { get; set; }
        public DbSet<GlPosting> GlPostings { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<ClientAccount> ClientAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ExpenseIncomeEntry> ExpenseIncomeEntries { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }


        //public DbSet<Role> Roles { get; set; }
        //public DbSet<RoleClaim> RoleClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasData(
                   new Customer
                   {
                       FullName = "James Bond",
                       Address = "Bermuda Triangle",
                       CustomerInfo = "smart",
                       ID = 1,
                       CustomerLongID = "64545566",
                       Email = "Jamesbond007@gmail.com",
                       Gender = Core.Enums.Gender.Male,
                       PhoneNumber = "007007007007",
                   }
            );


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));


                foreach (var property in properties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasColumnType("decimal(18,2)");
                }
                //}
            }

        }
    }
}
