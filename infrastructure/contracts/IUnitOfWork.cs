using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.contracts
{
    public interface IUnitOfWork
        {/// <summary>
         /// Asynchronously saves all changes made in this context to the underlying database.
         /// </summary>
         /// <returns>A task representing the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Begins a new database transaction.
        /// </summary>
        /// <returns>The transaction object.</returns>
        IDbContextTransaction BeginTransaction();

        /// <summary>
        /// Asynchronously begins a new database transaction.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains the transaction object.</returns>
        Task<IDbContextTransaction> BeginTransactionAsync();

        IVehicleRepository VehicleRepository { get; }
        IVehicleHistoryRepository VehicleHistoryRepository { get; }
        IParkingSlotRepository ParkingSlotRepository { get; }
    }
}
