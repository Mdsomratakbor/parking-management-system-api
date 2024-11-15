using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utilities.constants;

namespace domain.dto
{
    public class VehicleDto
    {

        [Required]
        [MaxLength(20)]
        public string LicenseNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string VehicleType { get; set; }

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
        public string Status { get; set; } 

        [Required]
        public DateTime EntryTime { get; set; }

        public DateTime? ExitTime { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal ParkingCharge { get; set; }
    }
}
