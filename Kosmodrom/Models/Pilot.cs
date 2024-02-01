using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kosmodrom.Models
{
    public partial class Pilot
    {
        public Pilot()
        {
            PassengerDeparturePilot1s = new HashSet<PassengerDeparture>();
            PassengerDeparturePilot2s = new HashSet<PassengerDeparture>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string FirstName { get; set; } = null!;
        [StringLength(255)]
        [Unicode(false)]
        public string LastName { get; set; } = null!;
        public bool LeadPilot { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string Sex { get; set; } = null!;
        [StringLength(255)]
        [Unicode(false)]
        public string Nationality { get; set; } = null!;
        public int Age { get; set; }
        public int Experience { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastEditedAt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DeletedAt { get; set; }

        [InverseProperty("Pilot1")]
        public virtual ICollection<PassengerDeparture> PassengerDeparturePilot1s { get; set; }
        [InverseProperty("Pilot2")]
        public virtual ICollection<PassengerDeparture> PassengerDeparturePilot2s { get; set; }
    }
}
