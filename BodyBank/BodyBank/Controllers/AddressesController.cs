using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BodyBank.Data;
using BodyBank.Models;
using Microsoft.AspNetCore.Authorization;
using BodyBank.Authentification;
using System.Security.Claims;

namespace BodyBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : CustomController
    {
        private readonly MVCBodyBankContext _context;

        public AddressesController(MVCBodyBankContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Addresse>> Get(int? id)
        {
            if (_context == null)
                return BadRequest("Context est null");

            if (id == null)
            {
                return _context.Addresse.ToArray();
            }
            return _context.Addresse.Where(x => x.AddresseId == id).ToArray();
        }

        [HttpPost]
        public async Task<ActionResult<Addresse>> Post([Bind("NoCivique,Rue,Ville,Province")] Addresse addresse)
        {

            if (_context == null)
                return BadRequest("Context est null");
            if (!IsAdmin())
                return BadRequest("Vous etes pas administrateur");
            if (!ModelState.IsValid)
                return BadRequest("L'addresse est mal construi");

            _context.Addresse.Add(addresse);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context == null)
                return BadRequest("Context est null");
            if (!IsAdmin())
                return BadRequest("Vous etes pas administrateur");

            var addresse = _context.Addresse.Find(id);
            if (addresse == null)
            {
                return BadRequest("Type existe pas");
            }

            _context.Addresse.Remove(addresse);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}
