﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    public class ItemBase
    {

        [Required]
        public string ProductId { get; set; } = null!;
        [MaybeNull]
        public Product Product { get; set; } = null!;


        [MaxLength(1024)]
        public string? Description { get; set; }


        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Amount { get; set; }

    }

}
