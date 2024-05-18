using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BodyBank.Data;
using BodyBank.Model;
using Microsoft.AspNetCore.Authorization;
using BodyBank.Authentification;
using System.Security.Claims;

namespace BodyBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonneursController : CustomController
    {
        private readonly MVCBodyBankContext _context;

        public DonneursController(MVCBodyBankContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donneur>>> Get(int? id)
        {
            if (_context.Donneur == null)
                return BadRequest("Context est null");

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
            if (_context == null)
                return BadRequest("Context est null");
            if (!IsAdmin())
                return BadRequest("Vous etes pas administrateur");
            if (!ModelState.IsValid)
                return BadRequest("Donneur mal construit");

            _context.Add(donneur);
            await _context.SaveChangesAsync();
            return Ok("Donneur creer");
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context == null)
                return BadRequest("Context est null");
            if (!IsAdmin())
                return BadRequest("Vous etes pas administrateur");

            var donneur = await _context.Donneur.FindAsync(id);
            if (donneur != null)
            {
                _context.Donneur.Remove(donneur);
            }
            
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
