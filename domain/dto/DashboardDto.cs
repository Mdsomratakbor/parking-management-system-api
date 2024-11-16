using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.dto
{
    public class DashboardDto
    {
        public int TotalCarsParked { get; set; }
        public int TotalEmptySlots { get; set; }
        public List<VehicleTypeInfoDto> VehicleTypeInfo { get; set; }
        public int VehiclesParkedMoreThanTwoHours { get; set; }
        public List<PieChartData> PieChart { get; set; }
        public List<LineChartData> LineChart { get; set; }
    }

    public class VehicleTypeInfoDto
    {
        public string VehicleType { get; set; }
        public int Count { get; set; }
    }
}
