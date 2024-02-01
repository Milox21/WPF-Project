using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kosmodrom.Models
{
    public partial class PassengerDeparture
    {
        public PassengerDeparture()
        {
            Reservations = new HashSet<Reservation>();
        }

        [Key]
        public int Id { get; set; }
        [Column(TypeName = "date")]
        public DateTime TimeOfDeparture { get; set; }
        [Column(TypeName = "date")]
        public DateTime TimeOfArrival { get; set; }
        public int? DestinationId { get; set; }
        public int? VehicleId { get; set; }
        public int? LandingPadReservationId { get; set; }
        public int? HangarReservationId { get; set; }
        public int? Pilot1Id { get; set; }
        public int? Pilot2Id { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastEditedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DeletedAt { get; set; }
        public double? Price { get; set; }

        [ForeignKey("DestinationId")]
        [InverseProperty("PassengerDepartures")]
        public virtual Destination? Destination { get; set; }
        [ForeignKey("HangarReservationId")]
        [InverseProperty("PassengerDepartures")]
        public virtual HangarReservation? HangarReservation { get; set; }
        [ForeignKey("LandingPadReservationId")]
        [InverseProperty("PassengerDepartures")]
        public virtual LandingPadsReservation? LandingPadReservation { get; set; }
        [ForeignKey("Pilot1Id")]
        [InverseProperty("PassengerDeparturePilot1s")]
        public virtual Pilot? Pilot1 { get; set; }
        [ForeignKey("Pilot2Id")]
        [InverseProperty("PassengerDeparturePilot2s")]
        public virtual Pilot? Pilot2 { get; set; }
        [ForeignKey("VehicleId")]
        [InverseProperty("PassengerDepartures")]
        public virtual Vehicle? Vehicle { get; set; }
        [InverseProperty("PassengerDepartures")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
