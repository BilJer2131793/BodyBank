using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BodyBank.Data;
using BodyBank.Model;
using BodyBank.Authentification;
using System.Security.Claims;

namespace BodyBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandeOrganesController : CustomController
    {
        private readonly MVCBodyBankContext _context;

        public CommandeOrganesController(MVCBodyBankContext context)
        {
            _context = context;
        }


        [HttpGet("{commandeId}")]
        public async Task<ActionResult<IEnumerable<Organne>>> Get(int commandeId)
        {
            if(_context == null)
                return BadRequest("Le context est null");

            var commande = _context.Commande.Where(x=>x.CommandeId == commandeId).FirstOrDefault();

            if(commande == null)
                return BadRequest("La commande existe pas");
            else if (!IsAdmin()&&commande.Util.UserName != GetUserName())
                return BadRequest("Vous avez pas acces a cette commande");

            var organnes = _context.CommandeOrgane
                .Where(x => x.Commande.CommandeId == commandeId)
                .Include(x => x.Organne)
                .Select(x => x.Organne).ToArray();

            return organnes;
        }

        [HttpPost("organneId, utilId")]
        public async Task<IActionResult> Post(int organneId, string? utilId)
        {
            if (_context.CommandeOrgane == null)
                return BadRequest("Le contexte est null");
            

            var organne = _context.Organne.Where(x => x.OrganneId == organneId).FirstOrDefault();
            var commande = _context.Commande.Where(x => x.Util.Id == utilId).LastOrDefault();

            if (organne == null)
                return BadRequest("Cette organne n'esxiste pas");
            else if (commande == null)
                return BadRequest("Cette commande n'existe pas");
            else if (!IsAdmin() && commande.Util.UserName != GetUserName())
                return BadRequest("Vous avez pas access a cette commande");

            _context.CommandeOrgane.Add(new CommandeOrgane { Organne = organne, Commande = commande });
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("organneId, utilId")]
        public async Task<IActionResult> Delete(int organneId, string? utilId)
        {
            if (_context.CommandeOrgane == null)
            {
                return BadRequest("Le contexte est null");
            }
            else if (organneId == null || utilId == null)
            {
                return BadRequest("Les informations envoyer sont invalide");
            }

            var organne = _context.Organne.Where(x => x.OrganneId == organneId).FirstOrDefault();
            var commande = _context.Commande.Where(x => x.Util.Id == utilId).LastOrDefault();

            if (organne == null)
                return BadRequest("Cette organne n'esxiste pas");
            else if (commande == null)
                return BadRequest("Cette commande n'existe pas");

            _context.CommandeOrgane.Remove(new CommandeOrgane { Organne = organne, Commande = commande });
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
