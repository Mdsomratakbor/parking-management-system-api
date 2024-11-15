using domain.dto;
using domain.entities;
using infrastructure.contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        // Get all vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetAllVehicles()
        {
            var vehicles = _unitOfWork.VehicleRepository.GetAll();
            return Ok(vehicles);
        }

        // Get a vehicle by its ID
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

        // Add a new vehicle
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
                // Add the vehicle to the repository
                _unitOfWork.VehicleRepository.Add(vehicle);

                // Save the changes to the database
                await _unitOfWork.SaveChangesAsync();

                // Return 201 Created with the vehicle's data
                return CreatedAtAction(nameof(GetVehicleById), new { id = vehicle.VehicleId }, vehicle);
            }
            catch (Exception ex)
            {

                // Return an Internal Server Error (500) if something goes wrong
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        // Update a vehicle (only when vehicle exits)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return BadRequest("Vehicle ID mismatch");
            }

            var existingVehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);
            if (existingVehicle == null)
            {
                return NotFound();
            }

            _unitOfWork.VehicleRepository.Update(vehicle);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        // Delete a vehicle
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

        // Get vehicles by their status (in/out)
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehiclesByStatus(string status)
        {
            var vehicles = await _unitOfWork.VehicleRepository.GetVehiclesByStatusAsync(status);
            return Ok(vehicles);
        }

        // Calculate parking charge for a specific vehicle
        [HttpGet("charge/{vehicleId}")]
        public async Task<ActionResult<decimal>> GetParkingCharge(int vehicleId)
        {
            //var charge = await _unitOfWork.VehicleRepository.CalculateParkingChargeAsync(vehicleId);
            return Ok();
        }
    }
}
