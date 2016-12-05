using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace UnitTesting.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double Amout { get; set; }

        [Required]
        [ForeignKey("From")]
        public int FromId { get; set; }

        [Required]
        [ForeignKey("To")]
        public int ToId { get; set; }

        public virtual Account From { get; set; }

        public virtual Account To { get; set; }
    }
}
