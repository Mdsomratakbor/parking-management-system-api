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
    public class ParkingRecord
    {
        [Key]
        public int RecordId { get; set; }

        [Required]
        public DateTime RecordDate { get; set; } // Represents the date of the summary record

        [Required]
        [MaxLength(20)]
        public VehicleType VehicleType { get; set; }

        [Required]
        public int TotalVehicles { get; set; }

        [Required]
        public int TotalDuration { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalCharges { get; set; }

        // Foreign key to Vehicle
        [Required]
        public int VehicleId { get; set; }

        // Navigation property for related vehicle
        public Vehicle Vehicle { get; set; }
    }
}
