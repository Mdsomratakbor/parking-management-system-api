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
    public interface IVehicleRepository:IRepository<Vehicle>
    {

        Task<IEnumerable<Vehicle>> GetVehiclesByStatusAsync(string status);
        Task<decimal> CalculateParkingChargeAsync(int vehicleId);
    }
}
