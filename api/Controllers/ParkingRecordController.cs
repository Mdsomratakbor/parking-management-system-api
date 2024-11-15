using domain.entities;
using infrastructure.contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingRecordController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParkingRecordController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Get all parking records
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingRecord>>> GetAllParkingRecords()
        {
            var records = await _parkingRecordRepository.GetParkingRecordsForTodayAsync();
            return Ok(records);
        }

        // Get parking record by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingRecord>> GetParkingRecordById(int id)
        {
            var record = await _parkingRecordRepository.GetParkingRecordByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        // Add a new parking record
        [HttpPost]
        public async Task<ActionResult<ParkingRecord>> AddParkingRecord([FromBody] ParkingRecord parkingRecord)
        {
            if (parkingRecord == null)
            {
                return BadRequest("Parking record data is null");
            }

            await _parkingRecordRepository.AddParkingRecordAsync(parkingRecord);
            return CreatedAtAction(nameof(GetParkingRecordById), new { id = parkingRecord.RecordId }, parkingRecord);
        }

        // Update an existing parking record (exit time should be updated when vehicle exits)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParkingRecord(int id, [FromBody] ParkingRecord parkingRecord)
        {
            if (id != parkingRecord.RecordId)
            {
                return BadRequest("Record ID mismatch");
            }

            var existingRecord = await _parkingRecordRepository.GetParkingRecordByIdAsync(id);
            if (existingRecord == null)
            {
                return NotFound();
            }

            await _parkingRecordRepository.UpdateParkingRecordAsync(parkingRecord);
            return NoContent();
        }

        // Delete a parking record
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingRecord(int id)
        {
            var record = await _parkingRecordRepository.GetParkingRecordByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            await _parkingRecordRepository.DeleteParkingRecordAsync(id);
            return NoContent();
        }

        // Get parking records for vehicles parked for more than 2 hours
        [HttpGet("more-than-two-hours")]
        public async Task<ActionResult<IEnumerable<ParkingRecord>>> GetVehiclesParkedForMoreThanTwoHours()
        {
            var records = await _parkingRecordRepository.GetVehiclesParkedForMoreThanTwoHoursAsync();
            return Ok(records);
        }

        // Get summary of parking records by date
        [HttpGet("summary/{date}")]
        public async Task<ActionResult<IEnumerable<ParkingRecordSummary>>> GetParkingSummaryByDate(DateTime date)
        {
            var summary = await _parkingRecordRepository.GetVehicleSummaryByDateAsync(date);
            return Ok(summary);
        }
    }
    }
