using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReadingRoomStore.Models;

namespace ReadingRoomStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class testsController : ControllerBase
    {
        private readonly ReadingRoomDBContext _context;

        public testsController(ReadingRoomDBContext context)
        {
            _context = context;
        }

        // GET: api/tests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<test>>> GetTests()
        {
            return await _context.Tests.ToListAsync();
        }

        // GET: api/tests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<test>> Gettest(string id)
        {
            var test = await _context.Tests.FindAsync(id);

            if (test == null)
            {
                return NotFound();
            }

            return test;
        }

        // PUT: api/tests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Puttest(string id, test test)
        {
            if (id != test.testId)
            {
                return BadRequest();
            }

            _context.Entry(test).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!testExists(id))
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

        // POST: api/tests
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<test>> Posttest(test test)
        {
            _context.Tests.Add(test);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (testExists(test.testId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Gettest", new { id = test.testId }, test);
        }

        // DELETE: api/tests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<test>> Deletetest(string id)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();

            return test;
        }

        private bool testExists(string id)
        {
            return _context.Tests.Any(e => e.testId == id);
        }
    }
}
