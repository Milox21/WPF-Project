using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kosmodrom.Models
{
    public partial class PrivateArrival
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "date")]
        public DateTime TimeOfArrival { get; set; }
        public int? DestinationId { get; set; }
        public int? LandingPadReservationId { get; set; }
        public int? HangarReservationId { get; set; }
        public int? CompanyId { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastEditedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("CompanyId")]
        [InverseProperty("PrivateArrivals")]
        public virtual CompanyLogIn? Company { get; set; }
        [ForeignKey("DestinationId")]
        [InverseProperty("PrivateArrivals")]
        public virtual Destination? Destination { get; set; }
        [ForeignKey("HangarReservationId")]
        [InverseProperty("PrivateArrivals")]
        public virtual HangarReservation? HangarReservation { get; set; }
        [ForeignKey("LandingPadReservationId")]
        [InverseProperty("PrivateArrivals")]
        public virtual LandingPadsReservation? LandingPadReservation { get; set; }
    }
}
