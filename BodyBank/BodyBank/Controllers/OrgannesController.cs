using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BodyBank.Data;
using BodyBank.Model;

namespace BodyBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrgannesController : ControllerBase
    {
        private readonly MVCBodyBankContext _context;

        public OrgannesController(MVCBodyBankContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organne>>> Get(int? id)
        {
            if (_context == null)
            {
                return BadRequest("Context is null");
            }
            if (id == null)
            {
                return _context.Organne
                    .Include(o => o.Type)
                    .Include(o => o.Donneur)
                    .ToArray(); ;
            }
            var organne = await _context.Organne
                .Include(o => o.Type)
                .Include(o => o.Donneur)
                .FirstOrDefaultAsync(m => m.OrganneId == id);
            if (organne == null)
            {
                return BadRequest("Aucun objet at cet Id");
            }

            return Ok(organne);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([Bind("Disponible,Prix,Type,Donneur")] Organne organne)
        {
            if (organne == null)
            {
                return BadRequest("Organne est pas bien contruit");
            }
            if (_context.Type.Contains(organne.Type) && _context.Donneur.Contains(organne.Donneur))
            {
                _context.Organne.Add(organne);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Le donneur ou le type d'organne n'esxiste pas");
            }

        }

        [HttpPut]
        public async Task<IActionResult> Put([Bind("OrganneId,Disponible,Prix,Type,Donneur")] Organne organne)
        {
            if(_context == null)
            {
                return BadRequest("Le context est null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("L'organne est mal construit");
            }

            _context.Organne.Update(organne);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context == null)
            {
                return BadRequest("Le context est null");
            }

            var organne = _context.Organne.Find(id);

            if (organne == null)
            {
                return BadRequest("Cette organne n'esxiste pas");
            }
            _context.Organne.Remove(organne);

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
