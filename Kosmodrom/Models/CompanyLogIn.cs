using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kosmodrom.Models
{
    public partial class CompanyLogIn
    {
        public CompanyLogIn()
        {
            BannedCompanies = new HashSet<BannedCompany>();
            HangarReservations = new HashSet<HangarReservation>();
            LandingPadsReservations = new HashSet<LandingPadsReservation>();
            PrivateArrivals = new HashSet<PrivateArrival>();
            PrivateDepartures = new HashSet<PrivateDeparture>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Login { get; set; } = null!;
        [StringLength(255)]
        [Unicode(false)]
        public string Password { get; set; } = null!;
        [StringLength(255)]
        [Unicode(false)]
        public string CompanyName { get; set; } = null!;
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

        [InverseProperty("Company")]
        public virtual ICollection<BannedCompany> BannedCompanies { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<HangarReservation> HangarReservations { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<LandingPadsReservation> LandingPadsReservations { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<PrivateArrival> PrivateArrivals { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<PrivateDeparture> PrivateDepartures { get; set; }
    }
}
