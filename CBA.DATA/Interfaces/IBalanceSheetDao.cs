using System;
using System.Collections.Generic;
using CBA.CORE.Models;

namespace CBA.DATA.Interfaces
{
    public interface IBalanceSheetDao
    {
        List<GLAccount> GetAssetAccounts();
        List<GLAccount> GetCapitalAccounts();
        List<LiabilityViewModel> GetLiabilityAccounts();
    }
}
