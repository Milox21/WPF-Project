using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kosmodrom.Models
{
    public partial class Reservation
    {
        [Key]
        public int Id { get; set; }
        public int SeatNumber { get; set; }
        public int? PassengerId { get; set; }
        public int? PassengerDeparturesId { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastEditedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("PassengerId")]
        [InverseProperty("Reservations")]
        public virtual PassengerLogIn? Passenger { get; set; }
        [ForeignKey("PassengerDeparturesId")]
        [InverseProperty("Reservations")]
        public virtual PassengerDeparture? PassengerDepartures { get; set; }
    }
}
