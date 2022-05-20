using System;
using System.ComponentModel.DataAnnotations;
using static CBA.CORE.Enums.Enums;

namespace CBA.CORE.Models
{
    public class TellerPosting
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "You must enter an amount")]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^[0-9.]+$", ErrorMessage = "Please enter a valid amount"), Range(1, (double)Decimal.MaxValue, ErrorMessage = ("Amount must be between 1 and a reasonable maximum value"))]
        public decimal Amount { get; set; }

        [DataType(DataType.MultilineText)]
        public string Narration { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Select a posting type")]
        public TellerPostingType PostingType { get; set; }

        [Display(Name = "Account")]
        public int ConsumerAccountID { get; set; }
        public virtual CustomerAccount CustomerAccount { get; set; }

        [Display(Name = "Post Initiator")]
        public string PostInitiatorId { get; set; }

        [Display(Name = "Till Account")]
        public int? TillAccountID { get; set; }
        public virtual GLAccount TillAccount { get; set; }

        [Display(Name = "Post Status")]
        public PostStatus Status { get; set; }
    }
}
