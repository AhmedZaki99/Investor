﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorData
{
    [Index(nameof(Name), IsUnique = true)]
    public class ScaleUnit : DatedEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;

        public Business? Business { get; set; }
        public string? BusinessId { get; set; }


        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;

        [MaxLength(1024)]
        public string? Description { get; set; }

    }
}
