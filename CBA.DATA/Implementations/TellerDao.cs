using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CBA.Core.Models;
using CBA.CORE.Models;
using CBA.Data;
using CBA.DATA.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CBA.DATA.Implementations
{
    public class TellerDao : ITellerDao
    {
        private readonly AppDbContext context;
        private readonly IGLAccountDao gLAccountDao;
        private readonly UserManager<ApplicationUser> userManager;

        public TellerDao(AppDbContext _context, IGLAccountDao _gLAccountDao, UserManager<ApplicationUser> _userManager)
        {
            context = _context;
            gLAccountDao = _gLAccountDao;
            userManager = _userManager;
        }

        public async Task<List<TillAccount>> GetAllTellerDetails()
        {
            List<TillAccount> output = new List<TillAccount>();
            var tillsWithTellers = GetDbTillAccounts();
            var tellersWithoutTill = await GetTellersWithNoTills();
            //var tellersWithTill = await GetTellersWithTills();

            //adding all tellers without a till account
            foreach (var teller in tellersWithoutTill)
            {
                output.Add(new TillAccount { UserId = teller.Id, GlAccountID = 0 });
            }

            //adding all tellers with a till account
            output.AddRange(tillsWithTellers);
            return output;
        }

        public async Task<List<ApplicationUser>> GetAllTellers()
        {
            var users = userManager.Users;

            List<ApplicationUser> tellers = new List<ApplicationUser>();

            foreach (var user in users)
            {
                var isInTellerRole = await userManager.IsInRoleAsync(user, "teller");
                if (isInTellerRole)
                {
                    tellers.Add(user);
                }
            }

            return (tellers);
        }

        public List<TillAccount> GetDbTillAccounts()
        {
            //return context.TillAccounts.ToList();
            return context.TillAccounts.Include(x => x.GlAccount).ToList();
        }

        public async Task<List<ApplicationUser>> GetTellersWithNoTills()
        {
            var tellers = await GetAllTellers();
            var tillAccounts = context.TillAccounts.ToList();
            var result = new List<ApplicationUser>();

            foreach (var teller in tellers)
            {
                if (!tillAccounts.Any(c => c.UserId == teller.Id))
                {
                    result.Add(teller);
                }
            }


            return result;
        }

        public async Task<List<ApplicationUser>> GetTellersWithTills()
        {
            var tellers = await GetAllTellers();
            var tillAccounts = context.TillAccounts.ToList();
            var result = new List<ApplicationUser>();

            foreach (var teller in tellers)
            {
                if (tillAccounts.Any(c => c.UserId == teller.Id))
                {
                    result.Add(teller);
                }
            }
            return result;
        }
    }
}
