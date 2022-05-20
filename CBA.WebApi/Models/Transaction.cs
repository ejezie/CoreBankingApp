using System;
using static CBA.CORE.Enums.Enums;

namespace CBA.CORE.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string AccountName { get; set; }
        public string SubCategory { get; set; }   
        public MainGLCategory MainCategory { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
