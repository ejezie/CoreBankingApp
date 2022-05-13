using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace CBA.DATA
{
    public class ClaimsStore
    {
        public static List<Claim> AllClaims = new()
        {
            new Claim("Add new user", "Add new user"),
            new Claim("Edit user information", "Edit user information"),
            new Claim("Change user passwords", "Change user passwords"),
            new Claim("Reset user password", "Reset user password"),
            new Claim("View list of users", "View list of users"),
            new Claim("Assign a Till account to a user", "Assign a Till account to a user"),
            new Claim("View list of tellers", "View list of tellers"),
            new Claim("Add a new customer", "Add a new customer"),
            new Claim("Edit customer info", "Edit customer info"),
            new Claim("View list of all customers", "View list of all customers"),
            new Claim("Add a new customer account", "Add a new customer account"),
            new Claim("Edit a customer account", "Edit a customer account"),
            new Claim("Close account", "Close account"),
            new Claim("View list of all customer accounts", "View list of all customer accounts"),
            new Claim("Edit the savings account type configuration", "Edit the savings account type configuration"),
            new Claim("Edit the current account type configuration", "Edit the current account type configuration"),
            new Claim("Edit the loan account type configuration", "Edit the loan account type configuration"),
            new Claim("Add new GL category", "Add new GL category"),
            new Claim("Edit GL category", "Edit GL category"),
            new Claim("View existing GL categories", "View existing GL categories"),
            new Claim("Add new GL account", "Add new GL account"),
            new Claim("Edit GL accounts", "Edit GL accounts"),
            new Claim("View existing GL accounts", "View existing GL accounts"),
            new Claim("Post transaction into GLs", "Post transaction into GLs"),
            new Claim("View list of GL postings", "View list of GL postings"),
            new Claim("Post transaction into customer accounts", "Post transaction into customer accounts"),
            new Claim("View list of teller postings", "View list of teller postings"),
            new Claim("Generate and view P&L report", "Generate and view P&L report"),
            new Claim("Generate and view balance sheet report", "Generate and view balance sheet report"),
            new Claim("Trial Balance", "Trial Balance"),
            new Claim("Close business", "Close business"),
            new Claim("Open business", "Open business")
        };
    }
}
