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
    public class ParkingSlot
    {
        public int ParkingSlotId { get; set; }  
        public string SlotNumber { get; set; }  
        public bool IsOccupied { get; set; }    
        public DateTime? OccupiedFrom { get; set; }  
        public DateTime? OccupiedUntil { get; set; }  

    }


}
