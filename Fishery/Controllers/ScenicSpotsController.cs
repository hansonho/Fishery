using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fishery.Models;

namespace Fishery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScenicSpotsController : ControllerBase
    {
        private readonly FisheryContext _context;

        public ScenicSpotsController(FisheryContext context)
        {
            _context = context;
        }

        // GET: api/ScenicSpots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScenicSpot>>> GetScenicSpot()
        {
            return await _context.ScenicSpot.ToListAsync();
        }

        // GET: api/ScenicSpots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScenicSpot>> GetScenicSpot(int id)
        {
            var scenicSpot = await _context.ScenicSpot.FindAsync(id);

            if (scenicSpot == null)
            {
                return NotFound();
            }

            return scenicSpot;
        }

        // PUT: api/ScenicSpots/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScenicSpot(int id, ScenicSpot scenicSpot)
        {
            if (id != scenicSpot.Id)
            {
                return BadRequest();
            }

            _context.Entry(scenicSpot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScenicSpotExists(id))
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

        // POST: api/ScenicSpots
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ScenicSpot>> PostScenicSpot(ScenicSpot scenicSpot)
        {
            _context.ScenicSpot.Add(scenicSpot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScenicSpot", new { id = scenicSpot.Id }, scenicSpot);
        }

        // DELETE: api/ScenicSpots/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ScenicSpot>> DeleteScenicSpot(int id)
        {
            var scenicSpot = await _context.ScenicSpot.FindAsync(id);
            if (scenicSpot == null)
            {
                return NotFound();
            }

            _context.ScenicSpot.Remove(scenicSpot);
            await _context.SaveChangesAsync();

            return scenicSpot;
        }

        private bool ScenicSpotExists(int id)
        {
            return _context.ScenicSpot.Any(e => e.Id == id);
        }
    }
}
