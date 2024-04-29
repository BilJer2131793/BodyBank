using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BodyBank.Data;
using BodyBank.Model;
using BodyBank.Models;

namespace BodyBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandesController : Controller
    {
        private readonly MVCBodyBankContext _context;

        public CommandesController(MVCBodyBankContext context)
        {
            _context = context;
        }

        [HttpGet("utilId,commandeId")]
        public async Task<ActionResult<IEnumerable<Commande>>> Get(int? utilId, int? commandeId)
        {
            if (_context.Commande == null)
            {
                return BadRequest("Context est null");
            }
            //renvoie toutes le commandes du system
            if(utilId == null)
            {
                return _context.Commande.ToArray();
            }
            //renvoie les commandes d'un utilisateur
            else if(commandeId == null)
            {
                var commandes = _context.Commande.Where(x => x.Util.UtilId == utilId).ToArray();
                if (commandes == null)
                    return BadRequest("Cette utilisateur a aucune commande");
                else 
                    return commandes;
            }

            var commande = _context.Commande.Where(x=>x.Util.UtilId == utilId && x.CommandeId == commandeId).ToArray();

            if (commande == null)
                return BadRequest("Cette commande n'existe pas");
            else
                return commande;
        }

        // GET: Commandes/Create
        [HttpPost("utilId")]
        public async Task<IActionResult> Post(int utilId)
        {
            if(utilId == null)
            {
                return BadRequest("Id est null");
            }

            var util = _context.Util.Where(x => x.UtilId == utilId).FirstOrDefault();
            if (util != null)
            {
                _context.Commande.Add(new Commande(util));
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Utilisateur invalide");
            }

        }


        [HttpPut("utilId,addresseId")]
        public async Task<IActionResult> FinalizerCommande(int utilId, int addresseId)
        {
            if (utilId == null)
            {
                return BadRequest("UtilisateurId est null");
            }
            else if(addresseId == null)
            {
                return BadRequest("addressId est null");
            }

            Util util = _context.Util.Where(x => x.UtilId == utilId).FirstOrDefault()!;
            Addresse addresse = _context.Addresse.Where(x =>x.AddresseId == addresseId).FirstOrDefault()!;

            if(util == null)
            {
                return BadRequest("Utilisateur invalide");
            }
            else if (addresse == null)
            {
                return BadRequest("Addresse invalide");
            }

            Commande commande = _context.Commande.LastOrDefault()!;
            Organne[] organnesCommande = _context.CommandeOrgane.Where(x => x.Commande.CommandeId == commande.CommandeId).Select(x=>x.Organne).ToArray();
            commande.Total = (decimal)organnesCommande.Sum(x => x.Prix);

            commande.AdresseLivraison = addresse;
            commande.Date = DateTime.Now.Date;

            _context.Commande.Update(commande);

            organnesCommande.ToList().ForEach(x => x.Disponible = false);
            _context.Organne.UpdateRange(organnesCommande);
            _context.Commande.Add(new Commande(util));

            await _context.SaveChangesAsync();
            return Ok();

        }


     

        private bool CommandeExists(int id)
        {
          return (_context.Commande?.Any(e => e.CommandeId == id)).GetValueOrDefault();
        }
    }
}
