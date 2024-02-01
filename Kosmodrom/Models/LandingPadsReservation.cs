using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kosmodrom.Models
{
    public partial class LandingPadsReservation
    {
        public LandingPadsReservation()
        {
            PassengerArrivals = new HashSet<PassengerArrival>();
            PassengerDepartures = new HashSet<PassengerDeparture>();
            PrivateArrivals = new HashSet<PrivateArrival>();
            PrivateDepartures = new HashSet<PrivateDeparture>();
        }

        [Key]
        public int Id { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartTime { get; set; }
        public int StartHour { get; set; }
        public int ReservationTime { get; set; }
        public int PadId { get; set; }
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
        [InverseProperty("LandingPadsReservations")]
        public virtual CompanyLogIn? Company { get; set; }
        [InverseProperty("LandingPadReservation")]
        public virtual ICollection<PassengerArrival> PassengerArrivals { get; set; }
        [InverseProperty("LandingPadReservation")]
        public virtual ICollection<PassengerDeparture> PassengerDepartures { get; set; }
        [InverseProperty("LandingPadReservation")]
        public virtual ICollection<PrivateArrival> PrivateArrivals { get; set; }
        [InverseProperty("LandingPadReservation")]
        public virtual ICollection<PrivateDeparture> PrivateDepartures { get; set; }
    }
}
