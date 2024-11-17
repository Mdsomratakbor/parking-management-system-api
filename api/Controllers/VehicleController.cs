using domain.dto;
using domain.entities;
using infrastructure.contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehicleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllVehicles(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var result = await _unitOfWork.VehicleRepository.GetPagedVehiclesAsync(pageNumber, pageSize);

                var response = new
                {
                    TotalRecords = result.TotalRecords,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Vehicles = result.Vehicles
                };

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicleById(int id)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }

        //[HttpPost]
        //public async Task<ActionResult<Vehicle>> AddVehicle([FromBody] VehicleDto vehicleDto)
        //{
        //    if (vehicleDto == null)
        //    {
        //        return BadRequest("Vehicle data is null");
        //    }

        //    var vehicle = new Vehicle
        //    {
        //        LicenseNumber = vehicleDto.LicenseNumber,
        //        VehicleType = vehicleDto.VehicleType,
        //        OwnerName = vehicleDto.OwnerName,
        //        OwnerPhone = vehicleDto.OwnerPhone,
        //        OwnerAddress = vehicleDto.OwnerAddress,
        //        Status = vehicleDto.Status,
        //        EntryTime = vehicleDto.EntryTime,
        //        ParkingCharge = vehicleDto.ParkingCharge,
        //        CreatedAt = DateTime.Now
        //    };

        //    try
        //    {
        //        if (vehicle.Status == "in")
        //        {
        //            var availableSlot = await _unitOfWork.ParkingSlotRepository
        //                .FirstOrDefaultAsync(slot => slot.IsOccupied == false);

        //            if (availableSlot != null)
        //            {
        //                vehicle.ParkingSlotId = availableSlot.ParkingSlotId;
        //                availableSlot.IsOccupied = true;
        //                availableSlot.OccupiedFrom = DateTime.Now;

        //                _unitOfWork.ParkingSlotRepository.Update(availableSlot);
        //            }
        //        }
        //        _unitOfWork.VehicleRepository.Add(vehicle);

        //        await _unitOfWork.SaveChangesAsync();

        //        return CreatedAtAction(nameof(GetVehicleById), new { id = vehicle.VehicleId }, vehicle);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred while processing your request.");
        //    }
        //}



        [HttpPost]
        public async Task<ActionResult<Vehicle>> AddVehicle([FromBody] VehicleDto vehicleDto)
        {
            if (vehicleDto == null)
            {
                return BadRequest("Vehicle data is null");
            }

            var vehicle = new Vehicle
            {
                LicenseNumber = vehicleDto.LicenseNumber,
                VehicleType = vehicleDto.VehicleType,
                OwnerName = vehicleDto.OwnerName,
                OwnerPhone = vehicleDto.OwnerPhone,
                OwnerAddress = vehicleDto.OwnerAddress,
                Status = vehicleDto.Status,
                EntryTime = vehicleDto.EntryTime,
                ParkingCharge = vehicleDto.ParkingCharge,
                CreatedAt = DateTime.Now
            };

            try
            {
                if (vehicle.Status == "in")
                {
                    var availableSlot = await _unitOfWork.ParkingSlotRepository
                        .FirstOrDefaultAsync(slot => slot.IsOccupied == false);

                    if (availableSlot != null)
                    {
                        vehicle.ParkingSlotId = availableSlot.ParkingSlotId;
                        availableSlot.IsOccupied = true;
                        availableSlot.OccupiedFrom = DateTime.Now;

                        _unitOfWork.ParkingSlotRepository.Update(availableSlot);
                    }
                }
                _unitOfWork.VehicleRepository.Add(vehicle);
                await _unitOfWork.SaveChangesAsync();
                var history = new VehicleHistory
                {
                    VehicleId = vehicle.VehicleId,
                    Status = vehicle.Status,
                    EntryTime = vehicle.EntryTime,
                    ParkingCharge = vehicle.ParkingCharge,
                    ParkingSlotId = vehicle.ParkingSlotId,
                    CreatedAt = DateTime.Now
                };

                _unitOfWork.VehicleHistoryRepository.Add(history);
                await _unitOfWork.SaveChangesAsync();

                return CreatedAtAction(nameof(GetVehicleById), new { id = vehicle.VehicleId }, vehicle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request."+ex.Message);
            }
        }



        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateVehicle(int id, [FromBody] VehicleUpdateDto vehicle)
        //{
        //    if (id != vehicle.VehicleId)
        //    {
        //        return BadRequest("Vehicle ID mismatch");
        //    }

        //    try
        //    {
        //        var existingVehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);
        //        if (existingVehicle == null)
        //        {
        //            return NotFound();
        //        }
        //        if (existingVehicle.Status == "in" && vehicle.Status == "out")
        //        {
        //            var parkingSlot = await _unitOfWork.ParkingSlotRepository
        //                .GetByIdAsync((int)existingVehicle.ParkingSlotId);

        //            if (parkingSlot != null)
        //            {
        //                parkingSlot.IsOccupied = false;
        //                parkingSlot.OccupiedUntil = DateTime.Now;

        //                _unitOfWork.ParkingSlotRepository.Update(parkingSlot);
        //            }
        //        }

        //        existingVehicle.LicenseNumber = vehicle.LicenseNumber;
        //        existingVehicle.VehicleType = vehicle.VehicleType;
        //        existingVehicle.OwnerName = vehicle.OwnerName;
        //        existingVehicle.OwnerPhone = vehicle.OwnerPhone;
        //        existingVehicle.OwnerAddress = vehicle.OwnerAddress;
        //        existingVehicle.Status = vehicle.Status;
        //        existingVehicle.EntryTime = vehicle.EntryTime;
        //        existingVehicle.ExitTime = vehicle.ExitTime;
        //        existingVehicle.ParkingCharge = vehicle.ParkingCharge;

        //        _unitOfWork.VehicleRepository.Update(existingVehicle);
        //        await _unitOfWork.SaveChangesAsync();

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred while processing your request.");
        //    }
        //}



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] VehicleUpdateDto vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return BadRequest("Vehicle ID mismatch");
            }

            try
            {
                var existingVehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);
                if (existingVehicle == null)
                {
                    return NotFound();
                }

                if (existingVehicle.Status == "in" && vehicle.Status == "out")
                {
                    var parkingSlot = await _unitOfWork.ParkingSlotRepository
                        .GetByIdAsync((int)existingVehicle.ParkingSlotId);

                    if (parkingSlot != null)
                    {
                        parkingSlot.IsOccupied = false;
                        parkingSlot.OccupiedUntil = DateTime.Now;

                        _unitOfWork.ParkingSlotRepository.Update(parkingSlot);
                    }
                }
                var history = new VehicleHistory
                {
                    VehicleId = id,
                    Status = vehicle.Status,
                    EntryTime = existingVehicle.EntryTime,
                    ExitTime = vehicle.ExitTime,
                    ParkingCharge = vehicle.ParkingCharge,
                    ParkingSlotId = existingVehicle.ParkingSlotId,
                    CreatedAt = DateTime.Now
                };

                _unitOfWork.VehicleHistoryRepository.Add(history);

                existingVehicle.LicenseNumber = vehicle.LicenseNumber;
                existingVehicle.VehicleType = vehicle.VehicleType;
                existingVehicle.OwnerName = vehicle.OwnerName;
                existingVehicle.OwnerPhone = vehicle.OwnerPhone;
                existingVehicle.OwnerAddress = vehicle.OwnerAddress;
                existingVehicle.Status = vehicle.Status;
                existingVehicle.EntryTime = vehicle.EntryTime;
                existingVehicle.ExitTime = vehicle.ExitTime;
                existingVehicle.ParkingCharge = vehicle.ParkingCharge;

                _unitOfWork.VehicleRepository.Update(existingVehicle);
                await _unitOfWork.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request." + ex.Message);
            }
        }


        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardInfo( [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string interval = "daily")
        {
            var filterStartDate = startDate ?? DateTime.Today;
            var filterEndDate = endDate ?? DateTime.Today;
            var dashboard = await _unitOfWork.VehicleRepository.GetDashboardData(filterStartDate, filterEndDate, interval);
            return Ok(dashboard);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

             _unitOfWork.VehicleRepository.Delete(vehicle);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehiclesByStatus(string status)
        {
            var vehicles = await _unitOfWork.VehicleRepository.GetVehiclesByStatusAsync(status);
            return Ok(vehicles);
        }

        [HttpGet("charge/{vehicleId}")]
        public async Task<ActionResult<decimal>> GetParkingCharge(int vehicleId)
        {
            //var charge = await _unitOfWork.VehicleRepository.CalculateParkingChargeAsync(vehicleId);
            return Ok();
        }
    }
}
