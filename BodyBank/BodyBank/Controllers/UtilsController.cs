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
    public class UtilsController : CustomController
    {
        private readonly MVCBodyBankContext _context;

        public UtilsController(MVCBodyBankContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> Get(string? Id)
        {
            if (_context == null)
                return BadRequest("Context est null");
            if (!IsAdmin())
                return BadRequest("Vous etes pas administrateur");

            if (Id == null)
            {
                return _context.Util.ToList();
            }

            var util = await _context.Util.Where(x=>x.Id == Id).FirstOrDefaultAsync();

            return Ok(util);
        }

        // Quand u utilisateur est creer, creer une commande par defaut ---------------------

        //[HttpPost]
        //public async Task<ActionResult> Post([Bind("PrenomUtil,NomUtil,Email")] Authentification.Utilisateur util)
        //{
        //    if (_context == null)
        //    {
        //        return BadRequest("Le context est null");
        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("L'utilisateur est mal construi");
        //    }

        //    _context.Util.Add(util);
        //    _context.Commande.Add(new Commande(util));
        //    _context.SaveChanges();


        //    return Ok();
        //}

        [HttpPut]
        public async Task<ActionResult> Put([Bind("UtilId,PrenomUtil,NomUtil,Email,AdresseUtil")]Utilisateur util)
        {
            if (_context == null)
                return BadRequest("Context est null");
            if (!IsAdmin())
                return BadRequest("Vous etes pas administrateur");
            if (!ModelState.IsValid)
                return BadRequest("L'utilisateur est mal construi");

            _context.Util.Update(util);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
