using domain.dto;
using domain.entities;
using Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utilities.constants;

namespace infrastructure.contracts
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {

        Task<IEnumerable<Vehicle>> GetVehiclesByStatusAsync(string status);
        Task<DashboardDto> GetDashboardData(DateTime startDate, DateTime endDate, string interval);
        Task<(IEnumerable<Vehicle> Vehicles, int TotalRecords)> GetPagedVehiclesAsync(int pageNumber, int pageSize);
        Task<List<LineChartData>> GetLineChartDataAsync(DateTime startDate, DateTime endDate, string interval);
        Task<List<PieChartData>> GetPieChartDataAsync();
        Task<List<Vehicle>> GetVehiclesParkedMoreThanTwoHoursAsync();
    }
}
