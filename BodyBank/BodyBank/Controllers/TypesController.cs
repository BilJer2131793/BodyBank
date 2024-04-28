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


namespace BodyBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypesController : ControllerBase
    {
        private readonly MVCBodyBankContext _context;

        public TypesController(MVCBodyBankContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Model.Type>> Get(int? id)
        {
            if(_context == null)
            {
                return BadRequest("Le context est null");
            }
            else if(id == null)
            {
                return _context.Type.ToArray();
            }
            return _context.Type.Where(x=>x.TypeId == id).ToArray();
        }

        [HttpPost]
        public async Task<ActionResult<Model.Type>> PostType([Bind("Nom,PrixBase,Desc,Image")]Model.Type type)
        {

            if (_context == null)
            {
                return BadRequest("Context est null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Type est mal construi");
            }
            _context.Type.Add(type);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            if(_context == null)
            {
                return BadRequest("Le context est null");
            }

            var type = _context.Type.Find(id);
            if (type == null)
            {
                return BadRequest("Type existe pas");
            }

            _context.Type.Remove(type);
            await _context.SaveChangesAsync();

            return Ok();

        }



        private bool TypeExists(int id)
        {
          return (_context.Type?.Any(e => e.TypeId == id)).GetValueOrDefault();
        }
    }
}
