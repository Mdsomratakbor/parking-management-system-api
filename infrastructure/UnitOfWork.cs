using infrastructure.contracts;
using infrastructure.repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        private readonly DataContext context;
        private readonly IConfiguration _configuration;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="configuration">The configuration settings.</param>
        public UnitOfWork(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            _configuration = configuration;
        }


        /// <summary>
        /// Begins a new database transaction.
        /// </summary>
        /// <returns>The transaction object.</returns>
        public IDbContextTransaction BeginTransaction()
        {
            return context.Database.BeginTransaction();
        }

        /// <summary>
        /// Asynchronously begins a new database transaction.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains the transaction object.</returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await context.Database.BeginTransactionAsync();
        }


        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        #region VehicleRepository
        private IVehicleRepository vehicleRepository;

        public IVehicleRepository VehicleRepository
        {
            get
            {
                if (vehicleRepository == null)
                    vehicleRepository = new VehicleRepository(context);

                return vehicleRepository;
            }
        }
        #endregion

        #region VehicleHistoryRepository
        private IVehicleHistoryRepository vehicleHistoryRepository;

        public IVehicleHistoryRepository VehicleHistoryRepository
        {
            get
            {
                if (vehicleHistoryRepository == null)
                    vehicleHistoryRepository = new VehicleHistoryRepository(context);

                return vehicleHistoryRepository;
            }
        }
        #endregion


        #region ParkingSlotRepository
        private IParkingSlotRepository parkingSlotRepository;

        public IParkingSlotRepository ParkingSlotRepository
        {
            get
            {
                if (parkingSlotRepository == null)
                    parkingSlotRepository = new ParkingSlotRepository(context);

                return parkingSlotRepository;
            }
        }
        #endregion

        /// <summary>
        /// Disposes of the resources used by the unit of work.
        /// </summary>
        /// <param name="disposing">True if called from Dispose method; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                _disposed = true;
            }
        }

        /// <summary>
        /// Disposes of the resources used by the unit of work.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
