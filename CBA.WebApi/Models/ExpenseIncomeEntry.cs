using System;
using static CBA.CORE.Enums.Enums;

namespace CBA.CORE.Models
{
    public class ExpenseIncomeEntry
    {
        public int ID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string AccountName { get; set; }
        public PandLType EntryType { get; set; }
    }
}
