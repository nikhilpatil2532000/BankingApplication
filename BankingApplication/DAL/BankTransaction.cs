using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BankingApplication.DAL
{
    [Table("Bank_Transaction")]
    public partial class BankTransaction
    {
        [Key]
        public long TransactionId { get; set; }
        [Key]
        public long AccountNo { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public long Amount { get; set; }

        [ForeignKey(nameof(AccountNo))]
        [InverseProperty(nameof(BankAccount.BankTransactions))]
        public virtual BankAccount AccountNoNavigation { get; set; }
    }
}
