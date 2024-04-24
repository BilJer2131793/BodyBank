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


        [HttpGet]
        public ActionResult<IEnumerable<Model.Type>> Get()
        {
              return _context.Type.ToArray();
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Model.Type>> Get(int id)
        {
            return _context.Type.Where(x=>x.TypeId == id).ToArray();
        }


        [HttpPost]
        public async Task<ActionResult<Model.Type>> PostType(Model.Type type)
        {
            if(type == null)
            {
                return BadRequest("Type is mepty");
            }
            if (_context.Type == null)
            {
                return BadRequest("Context is null.");
            }
            _context.Type.Add(type);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            var type = _context.Type.Find(id);
            if (type == null)
            {
                return BadRequest("Type not found.");
            }
            else
            {
                _context.Type.Remove(type);
                await _context.SaveChangesAsync();

                return Ok();
            }
        }

        //// POST: Types/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("TypeId,Nom,PrixBase,Desc,Image")] Model.Type @type)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(@type);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(@type);
        //}



        //// GET: Types/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Type == null)
        //    {
        //        return NotFound();
        //    }

        //    var @type = await _context.Type.FindAsync(id);
        //    if (@type == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(@type);
        //}

        //// POST: Types/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("TypeId,Nom,PrixBase,Desc,Image")] Model.Type @type)
        //{
        //    if (id != @type.TypeId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(@type);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TypeExists(@type.TypeId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(@type);
        //}

        //// GET: Types/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Type == null)
        //    {
        //        return NotFound();
        //    }

        //    var @type = await _context.Type
        //        .FirstOrDefaultAsync(m => m.TypeId == id);
        //    if (@type == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(@type);
        //}

        //// POST: Types/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Type == null)
        //    {
        //        return Problem("Entity set 'MVCBodyBankContext.Type'  is null.");
        //    }
        //    var @type = await _context.Type.FindAsync(id);
        //    if (@type != null)
        //    {
        //        _context.Type.Remove(@type);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool TypeExists(int id)
        {
          return (_context.Type?.Any(e => e.TypeId == id)).GetValueOrDefault();
        }
    }
}
