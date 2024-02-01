using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kosmodrom.Models
{
    public partial class AdminLogIn
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Login { get; set; } = null!;
        [StringLength(255)]
        [Unicode(false)]
        public string Password { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime? LastLogin { get; set; }
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
