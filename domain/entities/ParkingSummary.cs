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
    public class ParkingSummary
    {
        [Key]
        public DateTime SummaryDate { get; set; } // Date of the summary.

        public int TotalParked { get; set; } = 0; // Total vehicles parked on the day.

        public int EmptySlots { get; set; } = 100; // Assuming 100 total slots by default.

        public int CarCount { get; set; } = 0;

        public int BikeCount { get; set; } = 0;

        public int TruckCount { get; set; } = 0;

        public int MicrobusCount { get; set; } = 0;
    }


}
