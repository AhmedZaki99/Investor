﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InvestorAPI.Data
{
    public class CustomerPayment : PaymentBase
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;

        [Required]
        public string BusinessId { get; set; } = null!;
        public Business Business { get; set; } = null!;


        [NotNull]
        public Customer? Customer { get; set; } 
        public string? CustomerId { get; set; }


        [Required]
        public string PaymentMethodId { get; set; } = null!;
        public PaymentMethod PaymentMethod { get; set; } = null!;

    }
}
