using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kosmodrom.Models
{
    public partial class Destination
    {
        public Destination()
        {
            Malfunctions = new HashSet<Malfunction>();
            PassengerArrivals = new HashSet<PassengerArrival>();
            PassengerDepartures = new HashSet<PassengerDeparture>();
            PrivateArrivals = new HashSet<PrivateArrival>();
            PrivateDepartures = new HashSet<PrivateDeparture>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        public double Distance { get; set; }
        public int? Delay { get; set; }
        public bool IsAvailable { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastEditedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DeletedAt { get; set; }

        [InverseProperty("Destination")]
        public virtual ICollection<Malfunction> Malfunctions { get; set; }
        [InverseProperty("Destination")]
        public virtual ICollection<PassengerArrival> PassengerArrivals { get; set; }
        [InverseProperty("Destination")]
        public virtual ICollection<PassengerDeparture> PassengerDepartures { get; set; }
        [InverseProperty("Destination")]
        public virtual ICollection<PrivateArrival> PrivateArrivals { get; set; }
        [InverseProperty("Destination")]
        public virtual ICollection<PrivateDeparture> PrivateDepartures { get; set; }
    }
}
