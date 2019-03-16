using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A350CEM_Course_Work.Models;

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
            var employees = from row in _context.Employees.Include(i => i.Team) select row;

            return await employees.ToListAsync();
        }

        [HttpGet("byEmployeeNumber")]
        public async Task<ActionResult<Employee>> GetEmployeeByEmployeeNumber(int employeeNumber)
        {
            var employees = from row in _context.Employees.Include(i => i.Team) where row.EmployeeNumber == employeeNumber select row;

            if (employees.Count() == 0)
            {
                return NotFound();
            }

            return await employees.FirstAsync();
        }

        [HttpGet("byTeamId")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByTeamId(int teamId)
        {
            var teams = from row in _context.Teams where row.Id == teamId select row;

            if (teams.Count() == 0)
            {
                return NotFound();
            }

            var employees = from row in _context.Employees.Include(i => i.Team) where row.Team.Id == teamId select row;

            return await employees.ToListAsync();
        }

        // Update
        [HttpPut]
        public async Task<IActionResult> PutEmployeeByEmployeeNumber(Employee employee)
        {
            // TODO(James Lee) - Do some form of verification here; don't just blindly update.

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("addToTeam")]
        public async Task<IActionResult> AddEmployeeToTeam(int employeeNumber, int teamId)
        {
            var employees = from row in _context.Employees where row.EmployeeNumber == employeeNumber select row;

            if (employees.Count() == 0)
            {
                return BadRequest();
            }

            var teams = from row in _context.Teams where row.Id == teamId select row;

            if (teams.Count() == 0)
            {
                return BadRequest();
            }

            var employee = await employees.FirstAsync();
            var team = await teams.FirstAsync();

            employee.Team = team;

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
