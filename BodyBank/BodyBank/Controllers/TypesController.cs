using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BodyBank.Data;
using BodyBank.Model;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Authorization;
using BodyBank.Authentification;
using System.Security.Claims;

namespace BodyBank.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TypesController : CustomController
    {
        private readonly MVCBodyBankContext _context;

        public TypesController(MVCBodyBankContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model.Type>>> Get(int? id)
        {

            if (_context == null)
                return BadRequest("Context est null");


            if(id == null)
            {
                return Ok(_context.Type.ToArray());
            }
            return Ok(_context.Type.Where(x=>x.TypeId == id).ToArray());
        }

        [HttpPost]
        public async Task<ActionResult<Model.Type>> PostType([Bind("Nom,PrixBase,Desc,Image")]Model.Type type)
        {

            if (_context == null)
                return BadRequest("Context est null");
            if (!IsAdmin())
                return BadRequest("Vous etes pas administrateur");
            if (!ModelState.IsValid)
                return BadRequest("Type est mal construi");

            _context.Type.Add(type);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Put([Bind("TypeId,Nom,PrixBase,Desc,Image")] Model.Type type)
        {
            if (_context == null)
                return BadRequest("Context is null");
            if (!IsAdmin())
                return BadRequest("Vous etes pas administrateur");
            if (!ModelState.IsValid)
                return BadRequest("Type est mal construit");

            try
            {
                _context.Entry(type).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return BadRequest("Ce type n'existe pas");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            if(_context == null)
                return BadRequest("Le context est null");
            if (!IsAdmin())
                return BadRequest("Vous etes pas administrateur");

            var type = _context.Type.Find(id);
            if (type == null)
            {
                return BadRequest("Type existe pas");
            }

            _context.Type.Remove(type);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /*private bool IsAdmin()
        {
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                var roles = currentUser.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
                return roles.Contains(RolesUtilisateur.Administrateur);
            }
            return false;
        }*/
    }
}
