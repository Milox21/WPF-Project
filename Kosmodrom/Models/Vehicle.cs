using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kosmodrom.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            PassengerArrivals = new HashSet<PassengerArrival>();
            PassengerDepartures = new HashSet<PassengerDeparture>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(255)]
        [Unicode(false)]
        public string Type { get; set; } = null!;
        public int SeatCount { get; set; }
        public double Speed { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastEditedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DeletedAt { get; set; }

        [InverseProperty("Vehicle")]
        public virtual ICollection<PassengerArrival> PassengerArrivals { get; set; }
        [InverseProperty("Vehicle")]
        public virtual ICollection<PassengerDeparture> PassengerDepartures { get; set; }
    }
}
