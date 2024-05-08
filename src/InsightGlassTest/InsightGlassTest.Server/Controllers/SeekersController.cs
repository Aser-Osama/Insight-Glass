using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsightGlassTest.Server.Models;

namespace InsightGlassTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeekersController : ControllerBase
    {
        private readonly idbcontext _context;

        public SeekersController(idbcontext context)
        {
            _context = context;
        }

        // GET: api/Seekers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seeker>>> GetSeekers()
        {
            return await _context.Seekers.ToListAsync();
        }

        // GET: api/Seekers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seeker>> GetSeeker(string id)
        {
            var seeker = await _context.Seekers.FindAsync(id);

            if (seeker == null)
            {
                return NotFound();
            }

            return seeker;
        }

        // PUT: api/Seekers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeeker(string id, Seeker seeker)
        {
            if (id != seeker.SeekerUserId)
            {
                return BadRequest();
            }

            _context.Entry(seeker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeekerExists(id))
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

        // POST: api/Seekers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Seeker>> PostSeeker(Seeker seeker)
        {
            _context.Seekers.Add(seeker);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SeekerExists(seeker.SeekerUserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSeeker", new { id = seeker.SeekerUserId }, seeker);
        }

        // DELETE: api/Seekers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeeker(string id)
        {
            var seeker = await _context.Seekers.FindAsync(id);
            if (seeker == null)
            {
                return NotFound();
            }

            _context.Seekers.Remove(seeker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeekerExists(string id)
        {
            return _context.Seekers.Any(e => e.SeekerUserId == id);
        }
    }
}
