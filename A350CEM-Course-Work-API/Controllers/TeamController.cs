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
    [Route("api/teams")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly Context _context;

        public TeamController(Context context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeamById), new Team { Id = team.Id }, team);
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return await _context.Teams.ToListAsync();
        }

        [HttpGet("byTeamId")]
        public async Task<ActionResult<Team>> GetTeamById(int teamId)
        {
            var teams = from row in _context.Teams where row.Id == teamId select row;

            if (teams.Count() == 0)
            {
                return NotFound();
            }

            return await teams.FirstAsync();
        }

        // Update
        [HttpPut("byTeamId")]
        public async Task<IActionResult> PutTeamByTeamId(int teamId, Team team)
        {
            // TODO(James Lee) - Do some form of verification here; don't just blindly update.

            _context.Entry(team).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete
        [HttpDelete("byTeamId")]
        public async Task<IActionResult> DeleteTeamById(int teamId)
        {
            var teams = from row in _context.Teams where row.Id == teamId select row;

            if (teams.Count() == 0)
            {
                return NotFound();
            }

            var toDelete = await teams.FirstAsync();

            _context.Teams.Remove(toDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
