using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BankingApplication.DAL
{
    [Table("Bank_Customer")]
    public partial class BankCustomer
    {
        public BankCustomer()
        {
            BankAccounts = new HashSet<BankAccount>();
        }

        [Key]
        public long CustomerId { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Mobile { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Gender { get; set; }
        [Required]
        [StringLength(50)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        public string PanCard { get; set; }
        [Required]
        [StringLength(50)]
        public string AadharCard { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDate { get; set; }

        [InverseProperty(nameof(BankAccount.Customer))]
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}
