using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kosmodrom.Models
{
    public partial class PassengerArrival
    {
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
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastEditedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("DestinationId")]
        [InverseProperty("PassengerArrivals")]
        public virtual Destination? Destination { get; set; }
        [ForeignKey("HangarReservationId")]
        [InverseProperty("PassengerArrivals")]
        public virtual HangarReservation? HangarReservation { get; set; }
        [ForeignKey("LandingPadReservationId")]
        [InverseProperty("PassengerArrivals")]
        public virtual LandingPadsReservation? LandingPadReservation { get; set; }
        [ForeignKey("VehicleId")]
        [InverseProperty("PassengerArrivals")]
        public virtual Vehicle? Vehicle { get; set; }
    }
}
