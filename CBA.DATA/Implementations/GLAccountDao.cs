using System;
using System.Collections.Generic;
using System.Linq;
using CBA.CORE.Enums;
using CBA.CORE.Models;
using CBA.Data;
using CBA.DATA.Interfaces;
using static CBA.CORE.Enums.Enums;

namespace CBA.DATA.Implementations
{
    public class GLAccountDao : IGLAccountDao
    {
        private readonly AppDbContext context;

        public GLAccountDao(AppDbContext context)
        {
            this.context = context;
        }

        public bool AnyGlIn(Enums.MainGLCategory mainCategory)
        {
            return context.GLAccounts.Any(gl => gl.GLCategory.MainGLCategory == mainCategory);
        }

        public List<GLAccount> GetAll()
        {
            var glAccountList = context.GLAccounts.ToList();
            return glAccountList;
        }

        public List<GLAccount> GetAllAssetAccounts()
        {
            var output = context.GLAccounts.Where(c => c.GLCategory.MainGLCategory == MainGLCategory.Asset).ToList();

            return output;
        }

        public List<GLAccount> GetAllExpenseAccounts()
        {
            var output = context.GLAccounts.Where(c => c.GLCategory.MainGLCategory == MainGLCategory.Expense).ToList();

            return output;
        }

        public List<GLAccount> GetAllIncomeAccounts()
        {
            var output = context.GLAccounts.Where(c => c.GLCategory.MainGLCategory == MainGLCategory.Income).ToList();

            return output;
        }

        public List<GLAccount> GetAllLiabilityAccounts()
        {
            var output = context.GLAccounts.Where(c => c.GLCategory.MainGLCategory == MainGLCategory.Liability).ToList();

            return output;
        }

        public List<GLAccount> GetAllTills()
        {
            var tills = context.GLAccounts.Where(c => c.AccountName.ToLower().Contains("till")).ToList();

            return tills;
        }

        public GLAccount GetById(int Id)
        {
            var glAccount = context.GLAccounts.SingleOrDefault(c => c.ID == Id);

            return glAccount;
        }

        public List<GLAccount> GetByMainCategory(Enums.MainGLCategory mainCategory)
        {
            return context.GLAccounts.Where(a => a.GLCategory.MainGLCategory == mainCategory).ToList();

        }

        public GLAccount GetByName(string Name)
        {
            var glAccountByName = context.GLAccounts.SingleOrDefault(c => c.AccountName == Name);

            return glAccountByName;
        }

        public GLAccount GetLastGlIn(Enums.MainGLCategory mainCategory)
        {
            return context.GLAccounts.Where(g => g.GLCategory.MainGLCategory == mainCategory).OrderByDescending(a => a.ID).First();
        }

        public List<GLAccount> GetTillsWithoutTellers()
        {
            var output = new List<GLAccount>();
            List<GLAccount> allTills = GetAllTills();
            var tillAccount = context.TillAccounts.ToList();

            foreach (var till in allTills)
            {
                if (!tillAccount.Any(c => c.GlAccountID == till.ID))
                {
                    output.Add(till);
                }
            }

            return output;
        }

        public bool IsGlCategoryIsDeletable(int id)
        {
            return GetAll().Any(c => c.GLCategoryID == id);
        }
    }
}
