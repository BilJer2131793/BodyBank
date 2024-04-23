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
    public class OrgannesController : Controller
    {
        private readonly MVCBodyBankContext _context;

        public OrgannesController(MVCBodyBankContext context)
        {
            _context = context;
        }

        // GET: Organnes
        public async Task<IActionResult> Index()
        {
              return _context.Organne != null ? 
                          View(await _context.Organne.ToListAsync()) :
                          Problem("Entity set 'MVCBodyBankContext.Organne'  is null.");
        }

        // GET: Organnes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Organne == null)
            {
                return NotFound();
            }

            var organne = await _context.Organne
                .FirstOrDefaultAsync(m => m.OrganneId == id);
            if (organne == null)
            {
                return NotFound();
            }

            return View(organne);
        }

        // GET: Organnes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organnes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrganneId,Disponible,Prix")] Organne organne)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organne);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organne);
        }

        // GET: Organnes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Organne == null)
            {
                return NotFound();
            }

            var organne = await _context.Organne.FindAsync(id);
            if (organne == null)
            {
                return NotFound();
            }
            return View(organne);
        }

        // POST: Organnes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrganneId,Disponible,Prix")] Organne organne)
        {
            if (id != organne.OrganneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organne);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganneExists(organne.OrganneId))
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
            return View(organne);
        }

        // GET: Organnes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Organne == null)
            {
                return NotFound();
            }

            var organne = await _context.Organne
                .FirstOrDefaultAsync(m => m.OrganneId == id);
            if (organne == null)
            {
                return NotFound();
            }

            return View(organne);
        }

        // POST: Organnes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Organne == null)
            {
                return Problem("Entity set 'MVCBodyBankContext.Organne'  is null.");
            }
            var organne = await _context.Organne.FindAsync(id);
            if (organne != null)
            {
                _context.Organne.Remove(organne);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganneExists(int id)
        {
          return (_context.Organne?.Any(e => e.OrganneId == id)).GetValueOrDefault();
        }
    }
}
