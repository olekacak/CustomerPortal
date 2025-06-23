using BlazorCRM.Data;
using CustomerPortal.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CustomerPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext cs;

        public CustomerController(ApplicationDbContext context)
        {
            cs = context;
        }

        // POST: api/customer/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] Customer customer)
        {
            if (customer == null || string.IsNullOrWhiteSpace(customer.name))
            {
                return BadRequest("Invalid customer data.");
            }

            if (customer.customerId == null || customer.customerId == 0)
            {
                // Register (insert new customer)
                customer.role = 0;
                customer.is_active = 1;  
                customer.is_delete = 0;

                cs.Customers.Add(customer);
                await cs.SaveChangesAsync();

                return Ok(new { message = "Customer registered successfully", customerId = customer.customerId });
            }
            else
            {
                // Edit (update existing customer)
                var existing = await cs.Customers.FindAsync(customer.customerId);

                if (existing == null)
                {
                    return NotFound("Customer not found for update.");
                }

                // Update properties (manually or using AutoMapper)
                existing.name = customer.name ?? existing.name;
                existing.email = customer.email ?? existing.email;
                existing.password = customer.password ?? existing.password;
                existing.phone = customer.phone ?? existing.phone;
                existing.address = customer.address ?? existing.address;
                existing.city = customer.city ?? existing.city;
                existing.state = customer.state ?? existing.state;
                existing.country = customer.country ?? existing.country;
                existing.dob = customer.dob ?? existing.dob;
                existing.gender = customer.gender ?? existing.gender;
                existing.occupation = customer.occupation ?? existing.occupation;

                await cs.SaveChangesAsync();

                return Ok(new { message = "Customer updated successfully", customerId = existing.customerId });
            }
        }

        [HttpPost("login")]
        public IActionResult LoginCustomer([FromBody] Customer frombody)
        {
            var login = cs.Customers
                          .Where(x => x.email == frombody.email && x.password == frombody.password)
                          .FirstOrDefault();

            if (login == null)
            {
                return Unauthorized("Invalid credentials");
            }

            if (login.is_active != 1 || login.is_delete != 0)
            {
                return Unauthorized("Your account has been deactivated or deleted.");
            }

            return Ok(new { message = "Login successful", userData = login });
        }

        [HttpGet("getCustomer")]
        public IActionResult getCustomer([FromQuery] int? option, string? search)
        {

            if (option == 1)
            {
                var customers = cs.Customers.ToList();
                return Ok(new { Customers = customers });
            }
            else if (option == 2)
            {
                var byState = cs.Customers
                    .Where(x => x.state == search)
                    .ToList();


                return Ok(new { Customers = byState });
            }
            else if (option == 3)
            {
                var byCountry = cs.Customers
                    .Where(x => x.country == search)
                    .ToList();

                return Ok(new { Customers = byCountry });

            }
            else
            {
                return BadRequest("Invalid option");
            }
        }

        [HttpGet("getCustomerData")]
        public IActionResult GetDashboardData()
        {
            var total = cs.Customers.Count();

            var byAge = cs.Customers
                .AsEnumerable() // <--- Converts query to in-memory processing
                .GroupBy(x =>
                {
                    if (!x.dob.HasValue) return "Unknown";

                    var age = DateTime.Now.Year - x.dob.Value.Year;
                    if (x.dob.Value.Date > DateTime.Now.AddYears(-age)) age--;

                    if (age < 18) return "Under 18";
                    if (age <= 25) return "18-25";
                    if (age <= 35) return "26-35";
                    if (age <= 45) return "36-45";
                    if (age <= 60) return "46-60";
                    return "Above 60";
                })
                .Select(g => new { AgeGroup = g.Key, Count = g.Count() })
                .ToList();

            var byState = cs.Customers
                .GroupBy(x => x.state)
                .Select(g => new { State = g.Key, Count = g.Count() })
                .ToList();

            var byCountry = cs.Customers
                .GroupBy(x => x.country)
                .Select(g => new { Country = g.Key, Count = g.Count() })
                .ToList();

            return Ok(new
            {
                return_set_01 = total, byAge, byState, byCountry
            });
        }

        [HttpPost("deleteAccount")]
        public async Task<IActionResult> deleteAccount([FromBody] Customer frombody)
        {
            var customer = cs.Customers
                .Where(x => x.customerId == frombody.customerId)
                .FirstOrDefault();

            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            customer.is_active = 0;
            customer.is_delete = 1;

            await cs.SaveChangesAsync();

            return Ok(new { message = "Customer soft-deleted successfully", customerId = customer.customerId });
        }

    }
}
