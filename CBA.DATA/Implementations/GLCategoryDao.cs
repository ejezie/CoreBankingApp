using System;
using System.Linq;
using CBA.CORE.Models;
using CBA.Data;
using CBA.DATA.Interfaces;

namespace CBA.DATA.Implementations
{
    public class GLCategoryDao : IGLCategoryDao
    {
        private readonly AppDbContext context;

        public GLCategoryDao(AppDbContext context)
        {
            this.context = context;
        }

        public GLCategory GetById(int Id)
        {
            var glCategory = context.GLCategories.SingleOrDefault(c => c.ID == Id);
            return glCategory;
        }

        public GLCategory GetByName(string categoryName)
        {
            GLCategory result = context.GLCategories.SingleOrDefault(c => c.Name == categoryName);
            return result;
        }
    }
}
