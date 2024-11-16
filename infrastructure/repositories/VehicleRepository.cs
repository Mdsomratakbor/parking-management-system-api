using domain.dto;
using domain.entities;
using infrastructure.contracts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utilities.constants;

namespace infrastructure.repositories
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        private readonly DataContext _context;

        public VehicleRepository(DataContext context):base(context)
        {
            _context = context;
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int vehicleId)
        {
            return await _context.Vehicles.FindAsync(vehicleId);
        }

        public async Task<List<PieChartData>> GetPieChartDataAsync()
        {
            return await _context.Vehicles
                .Where(v => v.Status == "in") 
                .GroupBy(v => v.VehicleType)
                .Select(g => new PieChartData
                {
                    VehicleType = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
        }

        public async Task<List<LineChartData>> GetLineChartDataAsync(DateTime startDate, DateTime endDate, string interval)
        {
            var vehicles = _context.Vehicles
                .Where(v => v.EntryTime.Date >= startDate.Date && v.EntryTime.Date <= endDate.Date);

            IQueryable<LineChartData> groupedQuery;

            switch (interval.ToLower())
            {
                case "daily":
                    groupedQuery = vehicles
                        .GroupBy(v => v.EntryTime.Date)
                        .Select(g => new LineChartData
                        {
                            TimePeriod = g.Key.ToString("yyyy-MM-dd"),
                            Count = g.Count()
                        });
                    break;

                case "weekly":
                    groupedQuery = vehicles
                        .GroupBy(v => new
                        {
                            Year = EF.Functions.DateDiffYear(new DateTime(1900, 1, 1), v.EntryTime),
                            Week = (v.EntryTime.DayOfYear - 1) / 7 + 1
                        })
                        .Select(g => new LineChartData
                        {
                            TimePeriod = $"{g.Key.Year}-W{g.Key.Week}",
                            Count = g.Count()
                        });
                    break;

                case "monthly":
                    groupedQuery = vehicles
                        .GroupBy(v => new
                        {
                            v.EntryTime.Year,
                            v.EntryTime.Month
                        })
                        .Select(g => new LineChartData
                        {
                            TimePeriod = $"{g.Key.Year}-{g.Key.Month:D2}",
                            Count = g.Count()
                        });
                    break;

                default:
                    throw new ArgumentException("Invalid interval. Use 'daily', 'weekly', or 'monthly'.");
            }

            return await groupedQuery.ToListAsync();
        }


        public async Task<(IEnumerable<Vehicle> Vehicles, int TotalRecords)> GetPagedVehiclesAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("Page number and page size must be greater than zero.");
            }

            var query = _context.Vehicles.AsQueryable();
            var totalRecords = await query.CountAsync();

            var vehicles = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (vehicles, totalRecords);
        }


        public async Task<IEnumerable<Vehicle>> GetVehiclesByStatusAsync(string status)
        {
            return await _context.Vehicles.Where(v => v.Status.ToString() == status).ToListAsync();
        }

        public async Task<DashboardDto> GetDashboardData(DateTime startDate, DateTime endDate, string interval = "daily")
        {

            var vehiclesQuery = _context.Vehicles.AsQueryable();

            vehiclesQuery = vehiclesQuery.Where(v => v.EntryTime.Date == startDate.Date);

            var totalCarsParked = await vehiclesQuery.CountAsync();

            var totalParkingSlots = await _context.ParkingSlots.CountAsync();
            var totalOccupiedSlots = await vehiclesQuery.CountAsync(v => v.Status == "in");
            var totalEmptySlots = totalParkingSlots - totalOccupiedSlots;

            var vehicleTypeInfo = await vehiclesQuery
                .GroupBy(v => v.VehicleType)
                .Select(g => new { VehicleType = g.Key, Count = g.Count() })
                .ToListAsync();

            var vehiclesParkedMoreThanTwoHours = await vehiclesQuery
                .Where(v => v.Status == "in" && v.EntryTime.AddHours(2) < DateTime.Now)
                .CountAsync();

            return new DashboardDto
            {
                TotalCarsParked = totalCarsParked,
                TotalEmptySlots = totalEmptySlots,
                VehicleTypeInfo = vehicleTypeInfo.Select(vt => new VehicleTypeInfoDto
                {
                    VehicleType = vt.VehicleType,
                    Count = vt.Count
                }).ToList(),
                VehiclesParkedMoreThanTwoHours = vehiclesParkedMoreThanTwoHours,
                PieChart = await GetPieChartDataAsync(),
                LineChart = await GetLineChartDataAsync(startDate, endDate, interval)
            };
        }
    }
}
