using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BodyBank.Data;
using BodyBank.Models;

namespace BodyBank.Controllers
{
    public class AddressesController : Controller
    {
        private readonly MVCBodyBankContext _context;

        public AddressesController(MVCBodyBankContext context)
        {
            _context = context;
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
              return _context.Adresse != null ? 
                          View(await _context.Adresse.ToListAsync()) :
                          Problem("Entity set 'MVCBodyBankContext.Adresse'  is null.");
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Adresse == null)
            {
                return NotFound();
            }

            var addresse = await _context.Adresse
                .FirstOrDefaultAsync(m => m.AddresseId == id);
            if (addresse == null)
            {
                return NotFound();
            }

            return View(addresse);
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddresseId,NoCivique,Rue,Ville,Province")] Addresse addresse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addresse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addresse);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Adresse == null)
            {
                return NotFound();
            }

            var addresse = await _context.Adresse.FindAsync(id);
            if (addresse == null)
            {
                return NotFound();
            }
            return View(addresse);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddresseId,NoCivique,Rue,Ville,Province")] Addresse addresse)
        {
            if (id != addresse.AddresseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addresse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddresseExists(addresse.AddresseId))
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
            return View(addresse);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Adresse == null)
            {
                return NotFound();
            }

            var addresse = await _context.Adresse
                .FirstOrDefaultAsync(m => m.AddresseId == id);
            if (addresse == null)
            {
                return NotFound();
            }

            return View(addresse);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Adresse == null)
            {
                return Problem("Entity set 'MVCBodyBankContext.Adresse'  is null.");
            }
            var addresse = await _context.Adresse.FindAsync(id);
            if (addresse != null)
            {
                _context.Adresse.Remove(addresse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddresseExists(int id)
        {
          return (_context.Adresse?.Any(e => e.AddresseId == id)).GetValueOrDefault();
        }
    }
}
