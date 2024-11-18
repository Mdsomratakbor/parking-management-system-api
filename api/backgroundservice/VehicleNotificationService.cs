using api.Hubs;
using infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.backgroundservices
{
  
        public class VehicleNotificationService : BackgroundService
        {
            private readonly IServiceProvider _serviceProvider;
            private readonly IHubContext<ParkingHub> _hubContext;

            public VehicleNotificationService(IServiceProvider serviceProvider, IHubContext<ParkingHub> hubContext)
            {
                _serviceProvider = serviceProvider;
                _hubContext = hubContext;
            }

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                        var vehicles = context.Vehicles
                            .Where(v => v.Status == "in" && v.EntryTime <= DateTime.Now.AddHours(-2))
                            .Select(v => new
                            {
                                v.LicenseNumber,
                                v.OwnerName,
                                v.EntryTime
                            })
                            .ToList();

                        if (vehicles.Any())
                        {
                            await _hubContext.Clients.All.SendAsync("ReceiveVehiclesParkedOverTwoHours", vehicles);
                        }
                    }

                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
            }
        }
}
