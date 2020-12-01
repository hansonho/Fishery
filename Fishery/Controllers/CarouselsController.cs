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
    public class CarouselsController : ControllerBase
    {
        private readonly FisheryContext _context;

        public CarouselsController(FisheryContext context)
        {
            _context = context;
        }

        // GET: api/Carousels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carousel>>> GetCarousel()
        {
            return await _context.Carousel.OrderBy(a => a.Seq).ToListAsync();
        }

        // GET: api/Carousels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carousel>> GetCarousel(int id)
        {
            var carousel = await _context.Carousel.FindAsync(id);

            if (carousel == null)
            {
                return NotFound();
            }

            return carousel;
        }

        // PUT: api/Carousels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarousel(int id, Carousel carousel)
        {
            if (id != carousel.Id)
            {
                return BadRequest();
            }

            _context.Entry(carousel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarouselExists(id))
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

        // POST: api/Carousels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Carousel>> PostCarousel(Carousel carousel)
        {
            _context.Carousel.Add(carousel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarousel", new { id = carousel.Id }, carousel);
        }

        // DELETE: api/Carousels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Carousel>> DeleteCarousel(int id)
        {
            var carousel = await _context.Carousel.FindAsync(id);
            if (carousel == null)
            {
                return NotFound();
            }

            _context.Carousel.Remove(carousel);
            await _context.SaveChangesAsync();

            return carousel;
        }

        private bool CarouselExists(int id)
        {
            return _context.Carousel.Any(e => e.Id == id);
        }
    }
}
