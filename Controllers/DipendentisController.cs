using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Blogic.Authentication;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [BasicAutorizationAttributes]
    [Route("api/[controller]")]
    [ApiController]
    public class DipendentisController : ControllerBase
    {
        private readonly DipendentiAziendaContext _context;

        public DipendentisController(DipendentiAziendaContext context)
        {
            _context = context;
        }

        // GET: api/Dipendentis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dipendenti>>> GetDipendentis()
        {
          if (_context.Dipendentis == null)
          {
              return NotFound();
          }
            return await _context.Dipendentis.Include(emp=>emp.Attivita).ToListAsync();
        }

        // GET: api/Dipendentis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dipendenti>> GetDipendenti(string id)
        {
          if (_context.Dipendentis == null)
          {
              return NotFound();
          }
            var dipendenti = await _context.Dipendentis.FindAsync(id);

            if (dipendenti == null)
            {
                return NotFound();
            }

            return dipendenti;
        }

        // PUT: api/Dipendentis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDipendenti(string id, Dipendenti dipendenti)
        {
            if (id != dipendenti.Matricola)
            {
                return BadRequest();
            }

            _context.Entry(dipendenti).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DipendentiExists(id))
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

        // POST: api/Dipendentis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dipendenti>> PostDipendenti(Dipendenti dipendenti)
        {
          if (_context.Dipendentis == null)
          {
              return Problem("Entity set 'DipendentiAziendaContext.Dipendentis'  is null.");
          }
            _context.Dipendentis.Add(dipendenti);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DipendentiExists(dipendenti.Matricola))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDipendenti", new { id = dipendenti.Matricola }, dipendenti);
        }

        // DELETE: api/Dipendentis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDipendenti(string id)
        {
            if (_context.Dipendentis == null)
            {
                return NotFound();
            }
            var dipendenti = await _context.Dipendentis.FindAsync(id);
            if (dipendenti == null)
            {
                return NotFound();
            }

            _context.Dipendentis.Remove(dipendenti);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DipendentiExists(string id)
        {
            return (_context.Dipendentis?.Any(e => e.Matricola == id)).GetValueOrDefault();
        }
    }
}
