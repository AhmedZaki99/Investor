﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    public class Bill : InvoiceBillBase, IStringId
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;

        [Required]
        public string BusinessId { get; set; } = null!;
        public Business Business { get; set; } = null!;


        [NotNull]
        public Vendor? Vendor { get; set; } = null!;
        public string? VendorId { get; set; } = null!;


        public List<BillItem> Items { get; set; } = new();

    }

}
