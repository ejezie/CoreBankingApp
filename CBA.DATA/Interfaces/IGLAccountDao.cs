using System;
using System.Collections.Generic;
using CBA.CORE.Models;
using static CBA.CORE.Enums.Enums;

namespace CBA.DATA.Interfaces
{
    public interface IGLAccountDao
    {
        List<GLAccount> GetAll();

        bool IsGlCategoryIsDeletable(int id);

        GLAccount GetLastGlIn(MainGLCategory mainCategory);

        bool AnyGlIn(MainGLCategory mainCategory);

        GLAccount GetByName(string Name);

        GLAccount GetById(int Id);

        List<GLAccount> GetAllTills();

        List<GLAccount> GetTillsWithoutTellers();

        List<GLAccount> GetAllAssetAccounts();

        List<GLAccount> GetAllIncomeAccounts();

        List<GLAccount> GetAllLiabilityAccounts();

        List<GLAccount> GetAllExpenseAccounts();

        List<GLAccount> GetByMainCategory(MainGLCategory mainCategory);

    }
}
