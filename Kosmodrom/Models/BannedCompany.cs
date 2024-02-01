using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kosmodrom.Models
{
    public partial class BannedCompany
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Reason { get; set; } = null!;
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastEditedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("CompanyId")]
        [InverseProperty("BannedCompanies")]
        public virtual CompanyLogIn Company { get; set; } = null!;
    }
}
