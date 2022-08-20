﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InvestorAPI.Data
{
    public class Bill : InvoiceBillBase
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BillId { get; set; } = null!;


        [NotNull]
        public Vendor? Vendor { get; set; } = null!;
        public string? VendorId { get; set; } = null!;


        public List<BillItem> Items { get; set; } = new();

    }

}