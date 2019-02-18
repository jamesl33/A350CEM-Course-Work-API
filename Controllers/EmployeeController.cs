using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A350CEM_Course_Work.Models;

using System;

namespace A350CEM_Course_Work.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly Context _context;

        public EmployeeController(Context context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Aircraft>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeByEmployeeNumber), new { id = employee.Id }, employee);
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        [HttpGet("byEmployeeNumber")]
        public async Task<ActionResult<Employee>> GetEmployeeByEmployeeNumber(int employeeNumber)
        {
            var employee = from row in _context.Employees where row.EmployeeNumber == employeeNumber select row;

            if (employee.Count() == 0)
            {
                return NotFound();
            }

            return await employee.FirstAsync();
        }

        // Update
        [HttpPut("byEmployeeNumber")]
        public async Task<IActionResult> PutEmployeeByEmployeeNumber(int employeeNumber, Employee employee)
        {
            // TODO(James Lee) - Do some form of verification here; don't just blindly update.

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete
        [HttpDelete("byEmployeeNumber")]
        public async Task<IActionResult> DeleteEmployeeByEmployeeNumber(int employeeNumber)
        {
            var employees = from row in _context.Employees where row.EmployeeNumber == employeeNumber select row;

            if (employees.Count() == 0)
            {
                return NotFound();
            }

            var toDelete = await employees.FirstAsync();

            _context.Employees.Remove(toDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
