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
    [Route("api/aircraft")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        private readonly Context _context;

        public AircraftController(Context context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Aircraft>> PostAircraft(Aircraft aircraft)
        {
            _context.Aircraft.Add(aircraft);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAircraftBySerialNumber), new { id = aircraft.Id }, aircraft);
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aircraft>>> GetAircraft()
        {
            return await _context.Aircraft.ToListAsync();
        }

        [HttpGet("bySerialNumber")]
        public async Task<ActionResult<Aircraft>> GetAircraftBySerialNumber(string serialNumber)
        {
            var aircraft = from row in _context.Aircraft where row.SerialNumber == serialNumber select row;

            if (aircraft.Count() == 0)
            {
                return NotFound();
            }

            return await aircraft.FirstAsync();
        }

        // Update
        [HttpPut("bySerialNumber")]
        public async Task<IActionResult> PutAircraftBySerialNumber(string serialNumber, Aircraft aircraft)
        {
            // TODO(James Lee) - Do some form of verification here; don't just blindly update.

            _context.Entry(aircraft).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete
        [HttpDelete("bySerialNumber")]
        public async Task<IActionResult> DeleteAircraftBySerialNumber(string serialNumber)
        {
            var aircraft = from row in _context.Aircraft where row.SerialNumber == serialNumber select row;

            if (aircraft.Count() == 0)
            {
                return NotFound();
            }

            var toDelete = await aircraft.FirstAsync();

            _context.Aircraft.Remove(toDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
