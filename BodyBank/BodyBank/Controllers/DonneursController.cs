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
    public class DonneursController : Controller
    {
        private readonly MVCBodyBankContext _context;

        public DonneursController(MVCBodyBankContext context)
        {
            _context = context;
        }

        // GET: Donneurs/Details/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donneur>>> Get(int? id)
        {
            if (_context.Donneur == null)
            {
                return BadRequest("Context est null");
            }
            if(id == null)
            {
                return _context.Donneur.ToArray();
            }

            var donneur = await _context.Donneur
                .FirstOrDefaultAsync(m => m.DonneurId == id);

            if (donneur == null)
            {
                return BadRequest("Aucun objet at cet Id");
            }

            return Ok(donneur);
        }

        // POST: Donneurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonneurId,Nom,Prenom,Sexe,Age,Poids,Taille")] Donneur donneur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donneur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donneur);
        }


        // POST: Donneurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonneurId,Nom,Prenom,Sexe,Age,Poids,Taille")] Donneur donneur)
        {
            if (id != donneur.DonneurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donneur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonneurExists(donneur.DonneurId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(donneur);
        }


        // POST: Donneurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Donneur == null)
            {
                return Problem("Entity set 'MVCBodyBankContext.Donneur'  is null.");
            }
            var donneur = await _context.Donneur.FindAsync(id);
            if (donneur != null)
            {
                _context.Donneur.Remove(donneur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonneurExists(int id)
        {
          return (_context.Donneur?.Any(e => e.DonneurId == id)).GetValueOrDefault();
        }
    }
}
