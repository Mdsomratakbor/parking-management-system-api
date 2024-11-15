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
        public async Task<ActionResult<Vehicle>> AddVehicle([FromBody] Vehicle vehicle)
        {
            if (vehicle == null)
            {
                return BadRequest("Vehicle data is null");
            }

            _unitOfWork.VehicleRepository.Add(vehicle);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVehicleById), new { id = vehicle.VehicleId }, vehicle);
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
            var charge = await _unitOfWork.VehicleRepository.CalculateParkingChargeAsync(vehicleId);
            return Ok(charge);
        }
    }
}
