using domain.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ParkingSlot> ParkingSlots { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.LicenseNumber)
                .IsUnique();


            var parkingSlots = new List<ParkingSlot>();
            for (int i = 1; i <= 200; i++)
            {
                parkingSlots.Add(new ParkingSlot
                {
                    ParkingSlotId = i,
                    SlotNumber = $"Slot-{i:D3}",
                    IsOccupied = false,
                    OccupiedFrom = null,
                    OccupiedUntil = null,
                });
            }

            modelBuilder.Entity<ParkingSlot>().HasData(parkingSlots);
        }
    }
}
