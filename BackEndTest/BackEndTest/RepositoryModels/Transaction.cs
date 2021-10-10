using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndTest.RepositoryModels
{
    public class Transaction
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [StringLength(3)]
        public string CurrenctCode { get; set; }

        public DateTime TransectionDate { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }
    }
}
