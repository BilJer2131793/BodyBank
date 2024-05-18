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
    public class OrgannesController : CustomController
    {
        private readonly MVCBodyBankContext _context;

        public OrgannesController(MVCBodyBankContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organne>>> Get(int? id)
        {
            if (_context == null)
                return BadRequest("Context est null");

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
        public async Task<IActionResult> Post([Bind("Disponible,Prix,Type,Donneur")] Organne organne)
        {
            if (_context == null)
                return BadRequest("Context est null");
            if (!IsAdmin())
                return BadRequest("Vous etes pas administrateur");
            if (!ModelState.IsValid)
                return BadRequest("L'organne est mal construit");

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
            if (_context == null)
                return BadRequest("Context est null");
            if (!IsAdmin())
                return BadRequest("Vous etes pas administrateur");
            if (!ModelState.IsValid)
                return BadRequest("L'organne est mal construit");

            try
            {
                _context.Organne.Update(organne);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest("Cette organne n'existe pas");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context == null)
                return BadRequest("Context est null");
            if (!IsAdmin())
                return BadRequest("Vous etes pas administrateur");

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
