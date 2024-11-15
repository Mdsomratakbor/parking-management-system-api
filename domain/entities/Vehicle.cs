using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utilities.constants;

namespace domain.entities
{
    public class Vehicle:BaseEntity
    {
        [Key]
        public int VehicleId { get; set; }

        [Required]
        [MaxLength(20)]
        public string LicenseNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public VehicleType VehicleType { get; set; }

        [Required]
        [MaxLength(50)]
        public string OwnerName { get; set; }

        [Required]
        [MaxLength(15)]
        public string OwnerPhone { get; set; }

        [MaxLength(100)]
        public string OwnerAddress { get; set; }

        [Required]
        [MaxLength(10)]
        public Status Status { get; set; } // "in" or "out"

        [Required]
        public DateTime EntryTime { get; set; }

        public DateTime? ExitTime { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal ParkingCharge { get; set; }

        public int? Duration { get; set; }

        public ICollection<ParkingRecord> ParkingRecords { get; set; }
    }
}
