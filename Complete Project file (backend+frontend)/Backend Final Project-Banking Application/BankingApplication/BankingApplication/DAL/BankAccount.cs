using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BankingApplication.DAL
{
    [Table("Bank_Account")]
    public partial class BankAccount
    {
        public BankAccount()
        {
            BankTransactions = new HashSet<BankTransaction>();
        }

        [Key]
        public long AccountNo { get; set; }
        public long CustomerId { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        public long TotalBalance { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ActivationDate { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty(nameof(BankCustomer.BankAccounts))]
        public virtual BankCustomer Customer { get; set; }
        [InverseProperty(nameof(BankTransaction.AccountNoNavigation))]
        public virtual ICollection<BankTransaction> BankTransactions { get; set; }
    }
}
