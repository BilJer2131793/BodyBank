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
    public class DonneursController : Controller
    {
        private readonly MVCBodyBankContext _context;

        public DonneursController(MVCBodyBankContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donneur>>> Get(int? id)
        {
            if (_context.Donneur == null)
            {
                return BadRequest("Context est null");
            }
            if(id == null)
            {
                return _context.Donneur.ToArray();
            }

            var donneur = await _context.Donneur
                .FirstOrDefaultAsync(m => m.DonneurId == id);

            if (donneur == null)
            {
                return BadRequest("Aucun objet at cet Id");
            }

            return Ok(donneur);
        }

        [HttpPost]
        public async Task<IActionResult> Post([Bind("DonneurId,Nom,Prenom,Sexe,Age,Poids,Taille")] Donneur donneur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donneur);
                await _context.SaveChangesAsync();
                return Ok("Donneur creer");
            }
            else
            {
                return BadRequest("Donneur mal formuler");
            }
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Donneur == null)
            {
                return Problem("Entity set 'MVCBodyBankContext.Donneur'  is null.");
            }
            var donneur = await _context.Donneur.FindAsync(id);
            if (donneur != null)
            {
                _context.Donneur.Remove(donneur);
            }
            
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool DonneurExists(int id)
        {
          return (_context.Donneur?.Any(e => e.DonneurId == id)).GetValueOrDefault();
        }
    }
}
