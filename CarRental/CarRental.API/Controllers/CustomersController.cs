using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Infrastructure.Data;
using CarRental.Core.Models;
using CarRental.Core.DTOs;

namespace CarRental.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CarRentalDbContext _context;

        public CustomersController(CarRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetCustomers()
        {
            var customers = await _context.Customers
                .Select(c => new CustomerResponseDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    City = c.City,
                    State = c.State,
                    ZipCode = c.ZipCode,
                    Country = c.Country,
                    DateOfBirth = c.DateOfBirth,
                    DriverLicenseNumber = c.DriverLicenseNumber,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToListAsync();

            return Ok(customers);
        }

        // GET: api/customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDto>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = new CustomerResponseDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                City = customer.City,
                State = customer.State,
                ZipCode = customer.ZipCode,
                Country = customer.Country,
                DateOfBirth = customer.DateOfBirth,
                DriverLicenseNumber = customer.DriverLicenseNumber,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };

            return Ok(customerDto);
        }

        // GET: api/customers/search?query=searchterm
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> SearchCustomers([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Search query is required");
            }

            var customers = await _context.Customers
                .Where(c => c.FirstName.Contains(query) || 
                           c.LastName.Contains(query) || 
                           c.Email.Contains(query) ||
                           c.PhoneNumber.Contains(query))
                .Select(c => new CustomerResponseDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    City = c.City,
                    State = c.State,
                    ZipCode = c.ZipCode,
                    Country = c.Country,
                    DateOfBirth = c.DateOfBirth,
                    DriverLicenseNumber = c.DriverLicenseNumber,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToListAsync();

            return Ok(customers);
        }

        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult<CustomerResponseDto>> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            // Check if email already exists
            if (await _context.Customers.AnyAsync(c => c.Email == createCustomerDto.Email))
            {
                return BadRequest("A customer with this email already exists");
            }

            var customer = new Customer
            {
                FirstName = createCustomerDto.FirstName,
                LastName = createCustomerDto.LastName,
                Email = createCustomerDto.Email,
                PhoneNumber = createCustomerDto.PhoneNumber,
                Address = createCustomerDto.Address,
                City = createCustomerDto.City,
                State = createCustomerDto.State,
                ZipCode = createCustomerDto.ZipCode,
                Country = createCustomerDto.Country,
                DateOfBirth = createCustomerDto.DateOfBirth,
                DriverLicenseNumber = createCustomerDto.DriverLicenseNumber,
                CreatedAt = DateTime.UtcNow
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var customerResponse = new CustomerResponseDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                City = customer.City,
                State = customer.State,
                ZipCode = customer.ZipCode,
                Country = customer.Country,
                DateOfBirth = customer.DateOfBirth,
                DriverLicenseNumber = customer.DriverLicenseNumber,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customerResponse);
        }

        // PUT: api/customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            // Check if email is being updated and if it already exists
            if (updateCustomerDto.Email != null && 
                updateCustomerDto.Email != customer.Email &&
                await _context.Customers.AnyAsync(c => c.Email == updateCustomerDto.Email))
            {
                return BadRequest("A customer with this email already exists");
            }

            if (updateCustomerDto.FirstName != null)
                customer.FirstName = updateCustomerDto.FirstName;
            if (updateCustomerDto.LastName != null)
                customer.LastName = updateCustomerDto.LastName;
            if (updateCustomerDto.Email != null)
                customer.Email = updateCustomerDto.Email;
            if (updateCustomerDto.PhoneNumber != null)
                customer.PhoneNumber = updateCustomerDto.PhoneNumber;
            if (updateCustomerDto.Address != null)
                customer.Address = updateCustomerDto.Address;
            if (updateCustomerDto.City != null)
                customer.City = updateCustomerDto.City;
            if (updateCustomerDto.State != null)
                customer.State = updateCustomerDto.State;
            if (updateCustomerDto.ZipCode != null)
                customer.ZipCode = updateCustomerDto.ZipCode;
            if (updateCustomerDto.Country != null)
                customer.Country = updateCustomerDto.Country;
            if (updateCustomerDto.DateOfBirth.HasValue)
                customer.DateOfBirth = updateCustomerDto.DateOfBirth.Value;
            if (updateCustomerDto.DriverLicenseNumber != null)
                customer.DriverLicenseNumber = updateCustomerDto.DriverLicenseNumber;

            customer.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // DELETE: api/customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
