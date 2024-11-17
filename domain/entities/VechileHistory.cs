using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.entities
{
    public class VehicleHistory
    {
        [Key]
        public int HistoryId { get; set; }

        [Required]
        public int VehicleId { get; set; } 
        public Vehicle Vehicle { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime EntryTime { get; set; }

        public DateTime? ExitTime { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal ParkingCharge { get; set; }

        public int? ParkingSlotId { get; set; }
        public ParkingSlot ParkingSlot { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
