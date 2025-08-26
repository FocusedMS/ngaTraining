using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Infrastructure.Data;
using CarRental.Core.Models;
using CarRental.Core.DTOs;

namespace CarRental.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly CarRentalDbContext _context;

        public VehiclesController(CarRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleResponseDto>>> GetVehicles()
        {
            var vehicles = await _context.Vehicles
                .Select(v => new VehicleResponseDto
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    VehicleType = v.VehicleType,
                    LicensePlate = v.LicensePlate,
                    Year = v.Year,
                    DailyRate = v.DailyRate,
                    IsAvailable = v.IsAvailable,
                    Description = v.Description,
                    CreatedAt = v.CreatedAt,
                    UpdatedAt = v.UpdatedAt
                })
                .ToListAsync();

            return Ok(vehicles);
        }

        // GET: api/vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleResponseDto>> GetVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            var vehicleDto = new VehicleResponseDto
            {
                Id = vehicle.Id,
                Make = vehicle.Make,
                Model = vehicle.Model,
                VehicleType = vehicle.VehicleType,
                LicensePlate = vehicle.LicensePlate,
                Year = vehicle.Year,
                DailyRate = vehicle.DailyRate,
                IsAvailable = vehicle.IsAvailable,
                Description = vehicle.Description,
                CreatedAt = vehicle.CreatedAt,
                UpdatedAt = vehicle.UpdatedAt
            };

            return Ok(vehicleDto);
        }

        // GET: api/vehicles/type/{vehicleType}
        [HttpGet("type/{vehicleType}")]
        public async Task<ActionResult<IEnumerable<VehicleResponseDto>>> GetVehiclesByType(string vehicleType)
        {
            var vehicles = await _context.Vehicles
                .Where(v => v.VehicleType.ToLower() == vehicleType.ToLower())
                .Select(v => new VehicleResponseDto
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    VehicleType = v.VehicleType,
                    LicensePlate = v.LicensePlate,
                    Year = v.Year,
                    DailyRate = v.DailyRate,
                    IsAvailable = v.IsAvailable,
                    Description = v.Description,
                    CreatedAt = v.CreatedAt,
                    UpdatedAt = v.UpdatedAt
                })
                .ToListAsync();

            return Ok(vehicles);
        }

        // GET: api/vehicles/type/{vehicleType}/available
        [HttpGet("type/{vehicleType}/available")]
        public async Task<ActionResult<IEnumerable<VehicleResponseDto>>> GetAvailableVehiclesByType(string vehicleType)
        {
            var vehicles = await _context.Vehicles
                .Where(v => v.VehicleType.ToLower() == vehicleType.ToLower() && v.IsAvailable)
                .Select(v => new VehicleResponseDto
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    VehicleType = v.VehicleType,
                    LicensePlate = v.LicensePlate,
                    Year = v.Year,
                    DailyRate = v.DailyRate,
                    IsAvailable = v.IsAvailable,
                    Description = v.Description,
                    CreatedAt = v.CreatedAt,
                    UpdatedAt = v.UpdatedAt
                })
                .ToListAsync();

            return Ok(vehicles);
        }

        // POST: api/vehicles
        [HttpPost]
        public async Task<ActionResult<VehicleResponseDto>> CreateVehicle(CreateVehicleDto createVehicleDto)
        {
            var vehicle = new Vehicle
            {
                Make = createVehicleDto.Make,
                Model = createVehicleDto.Model,
                VehicleType = createVehicleDto.VehicleType,
                LicensePlate = createVehicleDto.LicensePlate,
                Year = createVehicleDto.Year,
                DailyRate = createVehicleDto.DailyRate,
                Description = createVehicleDto.Description,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            var vehicleResponse = new VehicleResponseDto
            {
                Id = vehicle.Id,
                Make = vehicle.Make,
                Model = vehicle.Model,
                VehicleType = vehicle.VehicleType,
                LicensePlate = vehicle.LicensePlate,
                Year = vehicle.Year,
                DailyRate = vehicle.DailyRate,
                IsAvailable = vehicle.IsAvailable,
                Description = vehicle.Description,
                CreatedAt = vehicle.CreatedAt,
                UpdatedAt = vehicle.UpdatedAt
            };

            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicleResponse);
        }

        // PUT: api/vehicles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, UpdateVehicleDto updateVehicleDto)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            if (updateVehicleDto.Make != null)
                vehicle.Make = updateVehicleDto.Make;
            if (updateVehicleDto.Model != null)
                vehicle.Model = updateVehicleDto.Model;
            if (updateVehicleDto.VehicleType != null)
                vehicle.VehicleType = updateVehicleDto.VehicleType;
            if (updateVehicleDto.LicensePlate != null)
                vehicle.LicensePlate = updateVehicleDto.LicensePlate;
            if (updateVehicleDto.Year.HasValue)
                vehicle.Year = updateVehicleDto.Year.Value;
            if (updateVehicleDto.DailyRate.HasValue)
                vehicle.DailyRate = updateVehicleDto.DailyRate.Value;
            if (updateVehicleDto.IsAvailable.HasValue)
                vehicle.IsAvailable = updateVehicleDto.IsAvailable.Value;
            if (updateVehicleDto.Description != null)
                vehicle.Description = updateVehicleDto.Description;

            vehicle.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}
