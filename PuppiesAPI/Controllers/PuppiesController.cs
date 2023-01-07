using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppiesAPI.Models;

namespace PuppiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuppiesController : ControllerBase
    {
        private readonly PuppyContext _context;

        public PuppiesController(PuppyContext context)
        {
            _context = context;
        }

        // GET: api/Puppies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PuppyDTO>>> GetPuppies()
        {
            if (_context.Puppies == null)
            {
                return NotFound();
            }

            return await _context.Puppies
            .Select(x => PuppyToPuppyDTO(x))
            .ToListAsync();
        }

        // GET: api/Puppies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PuppyDTO>> GetPuppy(Guid id)
        {
            if (_context.Puppies == null)
            {
                return NotFound();
            }
            var puppy = await _context.Puppies.FindAsync(id);

            if (puppy == null)
            {
                return NotFound();
            }

            return (PuppyDTO) PuppyToPuppyDTO(puppy);
        }

        // PUT: api/Puppies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPuppy(Guid id, PuppyDTOnoID puppy)
        {
            var pupperino = await _context.Puppies.FindAsync(id);
            if (pupperino== null)
            {
                return NotFound();
            }

            if (puppy.Name is null &&
                puppy.Breed is null &&
                puppy.BirthDate is null)
            {
                return BadRequest();
            }
            
            if (puppy.Name is not null)
            {
                pupperino.Name = puppy.Name;
            }
            if (puppy.Breed is not null)
            {
                pupperino.Breed = puppy.Breed;
            }
            if (puppy.BirthDate is not null)
            {
                pupperino.BirthDate = StringDateParser(puppy.BirthDate);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuppyExists(id))
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

        // POST: api/Puppies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PuppyDTO>> PostPuppy(PuppyDTOnoID puppyDTO)
        {
            if (puppyDTO.Name is null &&
                puppyDTO.Breed is null &&
                puppyDTO.BirthDate is null)
            {
                return BadRequest();
            }

            var pupperino = new Puppy()
            {
                Name = puppyDTO.Name,
                Breed = puppyDTO.Breed,
                BirthDate = StringDateParser(puppyDTO.BirthDate)
            };

            if (_context.Puppies == null)
            {
            return Problem("Entity set 'PuppyContext.Puppies'  is null.");
            }

            _context.Puppies.Add(pupperino);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPuppy),
                new { id = pupperino.Id },
                PuppyToPuppyDTO(pupperino));
        }

        // DELETE: api/Puppies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePuppy(Guid id)
        {
            if (_context.Puppies == null)
            {
                return NotFound();
            }
            var puppy = await _context.Puppies.FindAsync(id);
            if (puppy == null)
            {
                return NotFound();
            }

            _context.Puppies.Remove(puppy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PuppyExists(Guid id)
        {
            return (_context.Puppies?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static PuppyDTO PuppyToPuppyDTO(Puppy puppy)
        {
            return new PuppyDTO()
            {
                Id = puppy.Id,
                Name = puppy.Name,
                Breed = puppy.Breed,
                BirthDate = puppy.BirthDate.ToString(),
            };
        }

        private static DateOnly? StringDateParser(string? date)
        {
            if (DateOnly.TryParse(date, out DateOnly parsedDate))
            {
                return parsedDate;
            }
            else
            {
                return null;
            }
        }
    }
}
