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
using Microsoft.AspNetCore.Authorization;
using BodyBank.Authentification;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace BodyBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandesController : CustomController
    {
        private readonly MVCBodyBankContext _context;

        public CommandesController(MVCBodyBankContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<IEnumerable<Commande>>> GetDetails(string? utilId, int? commandeId)
        {
            if (_context.Commande == null)
                return BadRequest("Context est null");
            if (GetUserName==null && !IsAdmin())
                return BadRequest("Vous avez pas acces a cette commande");

            if (IsAdmin())
            {
                return await GetAdmin(utilId,commandeId);
            }
            else
            {
                return await GetUtilisateur(commandeId);
            }

        }

        private async Task<ActionResult<IEnumerable<Commande>>> GetUtilisateur(int? commandeId)
        {
            string userName = GetUserName();

            //renvoie les commandes d'un utilisateur
            if (commandeId == null)
            {
                var commandes = _context.Commande.Where(x => x.Util.UserName == userName).ToArray();
                //Creer une commande vide si l'utilisateur en a pas
                if (commandes == null)
                {
                    Utilisateur util = _context.Util.Where(x => x.UserName == userName).FirstOrDefault();
                    _context.Commande.Add(new Commande(util));
                    await _context.SaveChangesAsync();

                    return Ok(_context.Commande.LastOrDefault());
                }
                else
                    return Ok(commandes);
            }

            var commande = _context.Commande.Where(x => x.Util.UserName == userName && x.CommandeId == commandeId).FirstOrDefault();

            if (commande == null)
                return BadRequest("Cette commande n'existe pas");
            else
                return Ok(commande);

        }

        private async Task<ActionResult<IEnumerable<Commande>>> GetAdmin(string? utilId, int? commandeId)
        {
            //toutes les commandes du systeme
            if (utilId == null)
            {
                return _context.Commande.ToArray();
            }
            //toutes les commandes dun utilisateur
            else if(utilId != null)
            {
                Utilisateur util = _context.Util.Where(x => x.Id == utilId).FirstOrDefault()!;
                if (util == null)
                    return BadRequest("Cette utilisateur n'existe pas");
                else
                    return _context.Commande.Where(x => x.Util.Id == utilId).ToArray();
            }
            //une commande
            else
            {
                Commande com = _context.Commande.Where(x => x.CommandeId == commandeId).FirstOrDefault()!;
                if (com == null)
                    return BadRequest("Cette commande n'existe pas");
                else
                    return Ok(com);

            }

        }

        //renvoie tous les items dune commande
        [HttpGet]
        [Route("Contenu")]
        public async Task<ActionResult<IEnumerable<Commande>>> GetContenu(int commandeId)
        {
            if (_context.Commande == null)
            {
                return BadRequest("Context est null");
            }

            var commande = _context.Commande.Where(x => x.CommandeId == commandeId).FirstOrDefault();

            if (GetUserName() != commande.Util.UserName && !IsAdmin())
                return BadRequest("Vous avez pas access a cette commande");

            var contenuCommande = _context.CommandeOrgane.Where(x => x.Commande.CommandeId == commandeId)
                .Include(x => x.Organne).Select(x => x.Organne).ToArray();

            if (commande == null)
                return BadRequest("Cette commande n'existe pas");
            else
                return Ok(contenuCommande);
        }

        //get le contenu



        [HttpPost("utilId")]
        public async Task<IActionResult> Post(string utilId)
        {
            if(utilId == null)
            {
                return BadRequest("Id est null");
            }

            var util = _context.Util.Where(x => x.Id == utilId).FirstOrDefault();
            if (util != null && (util.UserName == GetUserName() || IsAdmin()))
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


        [HttpPut]
        [Route("finalizer")]
        public async Task<IActionResult> FinalizerCommande(string? utilId, int addresseId)
        {

            if (_context == null)
                return BadRequest("Context est null");
            if (GetUserName == null && !IsAdmin())
                return BadRequest("Vous avez pas acces a cette commande");
            if (addresseId == null)
            {
                return BadRequest("addressId est null");
            }

            Utilisateur util;
            if (utilId == null)
                util = _context.Util.Where(x => x.UserName == GetUserName()).FirstOrDefault()!;
            else
                util = _context.Util.Where(x => x.Id == utilId).FirstOrDefault()!;

            Addresse addresse = _context.Addresse.Where(x => x.AddresseId == addresseId).FirstOrDefault()!;

            if (util == null)
            {
                return BadRequest("Utilisateur invalide");
            }
            else if (addresse == null)
            {
                return BadRequest("Addresse invalide");
            }

            Commande commande = _context.Commande.Where(x=>x.Util == util).LastOrDefault()!;
            Organne[] organnesCommande = _context.CommandeOrgane.Where(x => x.Commande.CommandeId == commande.CommandeId).Select(x => x.Organne).ToArray();
            commande.Total = (decimal)organnesCommande.Sum(x => x.Prix);

            commande.Statut = "Payé";
            commande.AdresseLivraison = addresse;
            commande.Date = DateTime.Now.Date;

            _context.Commande.Update(commande);

            //met les organnes indisponible
            organnesCommande.ToList().ForEach(x => x.Disponible = false);
            _context.Organne.UpdateRange(organnesCommande);

            //nouvelle commande
            _context.Commande.Add(new Commande(util));

            await _context.SaveChangesAsync();
            return Ok();

        }

    }
}
