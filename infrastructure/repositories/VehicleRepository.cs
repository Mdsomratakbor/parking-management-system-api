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





        public async Task<IEnumerable<Vehicle>> GetVehiclesByStatusAsync(string status)
        {
            return await _context.Vehicles.Where(v => v.Status.ToString() == status).ToListAsync();
        }

        //public async Task<decimal> CalculateParkingChargeAsync(int vehicleId)
        //{
        //    // Fetch the vehicle and its related parking record
        //    var vehicle = await _context.Vehicles
        //        .Include(v => v.ParkingRecords)
        //        .FirstOrDefaultAsync(v => v.VehicleId == vehicleId);

        //    if (vehicle == null || vehicle.ParkingRecords == null || vehicle.ParkingRecords.Count == 0)
        //    {
        //        throw new Exception("Vehicle or parking record not found.");
        //    }

        //    // Get the most recent parking record (for simplicity, assuming it's the exit record)
        //    var latestParkingRecord = vehicle.ParkingRecords.OrderByDescending(pr => pr.EntryTime).FirstOrDefault();

        //    if (latestParkingRecord == null)
        //    {
        //        throw new Exception("Parking record not found.");
        //    }

        //    // Calculate the duration in hours (for simplicity, assuming exit time is provided)
        //    var entryTime = latestParkingRecord.EntryTime;
        //    var exitTime = latestParkingRecord.ExitTime ?? DateTime.Now;  // Assuming exit time is nullable
        //    var durationInHours = (exitTime - entryTime).TotalHours;

        //    // Define rates based on vehicle type using enum
        //    decimal ratePerHour = vehicle.VehicleType switch
        //    {
        //        VehicleType.Car => 10,
        //        VehicleType.Truck => 15,
        //        VehicleType.Bike => 5,
        //        VehicleType.MicroBus => 12,
        //        _ => throw new Exception("Unknown vehicle type.")
        //    };

        //    // Calculate the parking charge based on the rate and duration
        //    decimal parkingCharge = (decimal)durationInHours * ratePerHour;

        //    // You can also apply rounding or additional rules if needed, e.g., rounding to two decimal places
        //    parkingCharge = Math.Round(parkingCharge, 2);

        //    return parkingCharge;
        //}
    }
}
