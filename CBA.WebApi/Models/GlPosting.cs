﻿using System;
using System.ComponentModel.DataAnnotations;
using static CBA.CORE.Enums.Enums;

namespace CBA.CORE.Models
{
    public class GlPosting
    {
        public int ID { get; set; }

        [DataType(DataType.Currency)]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Credit Amount must be between 0 and a maximum reasonable value")]
        [Display(Name = "Credit Amount")]
        public decimal CreditAmount { get; set; }

        [DataType(DataType.Currency)]
        [Compare("CreditAmount", ErrorMessage = "Debit Amount must be equal to Credit Amount")]
        [Display(Name = "Debit Amount")]
        public decimal DebitAmount { get; set; }

        [DataType(DataType.MultilineText)]
        public string Narration { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "GL Account to debit")]
        public int? DrGlAccountID { get; set; }
        public virtual GLAccount DrGlAccount { get; set; }

        [Display(Name = "GL Account to credit")]
        public int? CrGlAccountID { get; set; }
        public virtual GLAccount CrGlAccount { get; set; }

        [Display(Name = "Post Initiator")]
        public string PostInitiatorId { get; set; }

        [Display(Name = "Post Status")]
        public PostStatus Status { get; set; }
    }
}
