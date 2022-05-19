using System;
using System.ComponentModel.DataAnnotations;

namespace CBA.CORE.Models
{
    public class Branch
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public string Address { get; set; }
        public long SortCode { get; set; }
    }
}
