using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CBA.Core.Models;
using CBA.CORE.Models;

namespace CBA.DATA.Interfaces
{
    public interface ITellerDao
    {
        Task<List<ApplicationUser>> GetAllTellers();
        Task<List<ApplicationUser>> GetTellersWithNoTills();
        Task<List<TillAccount>> GetAllTellerDetails();
        List<TillAccount> GetDbTillAccounts();
    }
}
