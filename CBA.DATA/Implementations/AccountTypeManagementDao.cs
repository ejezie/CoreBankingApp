//using System;
//using CBA.CORE.Models;
//using CBA.Data;
//using CBA.DATA.Interfaces;

//namespace CBA.DATA.Implementations
//{
//    public class AccountTypeManagementDao : IAccountTypeManagementDao
//    {
//        private readonly AppDbContext context;

//        public AccountTypeManagementDao(AppDbContext context)
//        {
//            this.context = context;
//        }

//        public AccountTypeManagement GetFirst()
//        {
//            return context.AccountTypeManagements.First();
//        }
//    }
//}
