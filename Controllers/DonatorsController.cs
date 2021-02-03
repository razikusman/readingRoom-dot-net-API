using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReadingRoomStore.Models;

namespace ReadingRoomStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DonatorsController : ControllerBase
    {
        private readonly ReadingRoomDBContext _context;

        public DonatorsController(ReadingRoomDBContext context)
        {
            _context = context;
        }

        //get all
        // GET: api/Donators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donator>>> GetDonators()
        {
            return await _context.Donators.ToListAsync();
        }

        //get by id
        // GET: api/Donators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Donator>> GetDonator(string id)
        {
            var donator = await _context.Donators.FindAsync(id);

            if (donator == null)
            {
                return NotFound();
            }

            return donator;
        }

        //get by id
        // GET: api/GetDonatordetails/5
        [HttpGet("GetDonatordetails/{id}")]
        public async Task<ActionResult<Donator>> GetDonatordetails(string id)
        {
            var donator =  _context.Donators
                                        .Where(don => don.DonatorId == id)
                                        .FirstOrDefault();

            if (donator == null)
            {
                return NotFound();
            }

            return donator;
        }

        //get by id
        // GET: api/GetDonatordetail
        [HttpGet("GetDonatordetail")]
        //public async Task<ActionResult<Donator>> GetDonatordetail(string id , string password)
        public async Task<ActionResult<Donator>> GetDonatordetail()
        {

            //var donator = await _context.Donators
            //                            .Where(don => don.DonatorId == id && don.DonatorPassword == password)
            //                            .FirstOrDefaultAsync();

            string id = HttpContext.User.Identity.Name;
            var donator = await _context.Donators
                                        .Where(don => don.DonatorId == id)
                                        .FirstOrDefaultAsync();

            //donator.DonatorPassword = null;

            if (donator == null)
            {
                return NotFound();
            }

            return Ok(donator);
        }

        // PUT: api/Donators/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonator(string id, Donator donator)
        {
            if (id != donator.DonatorId)
            {
                return BadRequest();
            }

            _context.Entry(donator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonatorExists(id))
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

        // POST: api/Donators
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Donator>> PostDonator(Donator donator)
        {
            _context.Donators.Add(donator);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DonatorExists(donator.DonatorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDonator", new { id = donator.DonatorId }, donator);
        }

        // DELETE: api/Donators/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Donator>> DeleteDonator(string id)
        {
            var donator = await _context.Donators.FindAsync(id);
            if (donator == null)
            {
                return NotFound();
            }

            _context.Donators.Remove(donator);
            await _context.SaveChangesAsync();

            return donator;
        }

        private bool DonatorExists(string id)
        {
            return _context.Donators.Any(e => e.DonatorId == id);
        }
    }
}
