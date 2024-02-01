using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kosmodrom.Models
{
    public partial class Malfunction
    {
        [Key]
        public int Id { get; set; }
        public int DelayTime { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Reason { get; set; } = null!;
        public int? DestinationId { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastEditedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("DestinationId")]
        [InverseProperty("Malfunctions")]
        public virtual Destination? Destination { get; set; }
    }
}
