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
    public class CommandeOrganesController : Controller
    {
        private readonly MVCBodyBankContext _context;

        public CommandeOrganesController(MVCBodyBankContext context)
        {
            _context = context;
        }

        // GET: CommandeOrganes
        public async Task<IActionResult> Index()
        {
              return _context.CommandeOrgane != null ? 
                          View(await _context.CommandeOrgane.ToListAsync()) :
                          Problem("Entity set 'MVCBodyBankContext.CommandeOrgane'  is null.");
        }

        // GET: CommandeOrganes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CommandeOrgane == null)
            {
                return NotFound();
            }

            var commandeOrgane = await _context.CommandeOrgane
                .FirstOrDefaultAsync(m => m.CommandeOrganeId == id);
            if (commandeOrgane == null)
            {
                return NotFound();
            }

            return View(commandeOrgane);
        }

        // GET: CommandeOrganes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CommandeOrganes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommandeOrganeId")] CommandeOrgane commandeOrgane)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commandeOrgane);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(commandeOrgane);
        }

        // GET: CommandeOrganes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CommandeOrgane == null)
            {
                return NotFound();
            }

            var commandeOrgane = await _context.CommandeOrgane.FindAsync(id);
            if (commandeOrgane == null)
            {
                return NotFound();
            }
            return View(commandeOrgane);
        }

        // POST: CommandeOrganes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommandeOrganeId")] CommandeOrgane commandeOrgane)
        {
            if (id != commandeOrgane.CommandeOrganeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commandeOrgane);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommandeOrganeExists(commandeOrgane.CommandeOrganeId))
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
            return View(commandeOrgane);
        }

        // GET: CommandeOrganes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CommandeOrgane == null)
            {
                return NotFound();
            }

            var commandeOrgane = await _context.CommandeOrgane
                .FirstOrDefaultAsync(m => m.CommandeOrganeId == id);
            if (commandeOrgane == null)
            {
                return NotFound();
            }

            return View(commandeOrgane);
        }

        // POST: CommandeOrganes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CommandeOrgane == null)
            {
                return Problem("Entity set 'MVCBodyBankContext.CommandeOrgane'  is null.");
            }
            var commandeOrgane = await _context.CommandeOrgane.FindAsync(id);
            if (commandeOrgane != null)
            {
                _context.CommandeOrgane.Remove(commandeOrgane);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommandeOrganeExists(int id)
        {
          return (_context.CommandeOrgane?.Any(e => e.CommandeOrganeId == id)).GetValueOrDefault();
        }
    }
}
