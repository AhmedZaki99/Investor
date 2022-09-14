﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    public abstract class PaymentBase : DatedEntity
    {

        public int? Number { get; set; }

        [Required]
        public decimal Amount { get; set; }


        [Required]
        [Precision(0)]
        public DateTime PaymentDate { get; set; }


        [MaxLength(1024)]
        public string? Notes { get; set; }

    }
}