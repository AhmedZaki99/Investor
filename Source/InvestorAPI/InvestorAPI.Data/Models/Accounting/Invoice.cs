﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InvestorAPI.Data
{
    public class Invoice : InvoiceBillBase
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string InvoiceId { get; set; } = null!;

        [Required]
        public string BusinessId { get; set; } = null!;
        public Business Business { get; set; } = null!;


        [NotNull]
        public Customer? Customer { get; set; }
        public string? CustomerId { get; set; }


        public List<InvoiceItem> Items { get; set; } = new();

    }

}
