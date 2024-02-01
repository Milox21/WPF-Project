using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kosmodrom.Models
{
    [Table("SpaceportSupport")]
    public partial class SpaceportSupport
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Thing { get; set; } = null!;
        public int Amount { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastEditedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DeletedAt { get; set; }
    }
}
