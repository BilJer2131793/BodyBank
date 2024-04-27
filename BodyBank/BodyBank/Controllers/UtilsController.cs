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
    public class UtilsController : Controller
    {
        private readonly MVCBodyBankContext _context;

        public UtilsController(MVCBodyBankContext context)
        {
            _context = context;
        }

        // Quand u utilisateur est creer, creer une commande par defaut ---------------------

        [HttpGet("Id")]
        public async Task<ActionResult<IEnumerable<Util>>> Get(int? Id)
        {
            if(_context == null)
            {
                return BadRequest("Le context est null");
            }

            if(Id == null)
            {
                return _context.Util.ToArray();
            }

            var util = _context.Util.Where(x=>x.UtilId == Id).FirstOrDefault();

            return Ok(util);
        }

        [HttpPost]
        public async Task<ActionResult> Post([Bind("PrenomUtil,NomUtil,Email")] Util util)
        {
            if (_context == null)
            {
                return BadRequest("Le context est null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("L'utilisateur est mal construi");
            }

            _context.Util.Add(util);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([Bind("UtilId,PrenomUtil,NomUtil,Email,AdresseUtil")]Util util)
        {
            if (_context == null)
            {
                return BadRequest("Le context est null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("L'utilisateur est mal construi");
            }
            _context.Util.Update(util);
            _context.SaveChanges();

            return Ok();
        }

        private bool UtilExists(int id)
        {
          return (_context.Util?.Any(e => e.UtilId == id)).GetValueOrDefault();
        }
    }
}
