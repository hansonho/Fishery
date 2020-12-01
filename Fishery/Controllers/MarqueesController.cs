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
    public class MarqueesController : ControllerBase
    {
        private readonly FisheryContext _context;

        public MarqueesController(FisheryContext context)
        {
            _context = context;
        }

        // GET: api/Marquees
        [HttpGet]
        public async Task<ActionResult<string>> GetMarquee()
        {
            return await _context.Marquee.Select(a => a.Text).FirstOrDefaultAsync();
        }

        // GET: api/Marquees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Marquee>> GetMarquee(int id)
        {
            var marquee = await _context.Marquee.FindAsync(id);

            if (marquee == null)
            {
                return NotFound();
            }

            return marquee;
        }

        // PUT: api/Marquees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarquee(int id, Marquee marquee)
        {
            if (id != marquee.Id)
            {
                return BadRequest();
            }

            _context.Entry(marquee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarqueeExists(id))
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

        // POST: api/Marquees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Marquee>> PostMarquee(Marquee marquee)
        {
            _context.Marquee.Add(marquee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMarquee", new { id = marquee.Id }, marquee);
        }

        // DELETE: api/Marquees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Marquee>> DeleteMarquee(int id)
        {
            var marquee = await _context.Marquee.FindAsync(id);
            if (marquee == null)
            {
                return NotFound();
            }

            _context.Marquee.Remove(marquee);
            await _context.SaveChangesAsync();

            return marquee;
        }

        private bool MarqueeExists(int id)
        {
            return _context.Marquee.Any(e => e.Id == id);
        }
    }
}
