using System;
using CBA.CORE.Models;

namespace CBA.DATA.Interfaces
{
    public interface IGLCategoryDao
    {
        GLCategory GetByName(string categoryName);
        GLCategory GetById(int Id);
    }
}
