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

        // GET: Utils
        public async Task<IActionResult> Index()
        {
              return _context.Util != null ? 
                          View(await _context.Util.ToListAsync()) :
                          Problem("Entity set 'MVCBodyBankContext.Util'  is null.");
        }

        // GET: Utils/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Util == null)
            {
                return NotFound();
            }

            var util = await _context.Util
                .FirstOrDefaultAsync(m => m.UtilId == id);
            if (util == null)
            {
                return NotFound();
            }

            return View(util);
        }

        // GET: Utils/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utils/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UtilId,PrenomUtil,NomUtil,Email")] Util util)
        {
            if (ModelState.IsValid)
            {
                _context.Add(util);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(util);
        }

        // GET: Utils/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Util == null)
            {
                return NotFound();
            }

            var util = await _context.Util.FindAsync(id);
            if (util == null)
            {
                return NotFound();
            }
            return View(util);
        }

        // POST: Utils/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UtilId,PrenomUtil,NomUtil,Email")] Util util)
        {
            if (id != util.UtilId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(util);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilExists(util.UtilId))
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
            return View(util);
        }

        // GET: Utils/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Util == null)
            {
                return NotFound();
            }

            var util = await _context.Util
                .FirstOrDefaultAsync(m => m.UtilId == id);
            if (util == null)
            {
                return NotFound();
            }

            return View(util);
        }

        // POST: Utils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Util == null)
            {
                return Problem("Entity set 'MVCBodyBankContext.Util'  is null.");
            }
            var util = await _context.Util.FindAsync(id);
            if (util != null)
            {
                _context.Util.Remove(util);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilExists(int id)
        {
          return (_context.Util?.Any(e => e.UtilId == id)).GetValueOrDefault();
        }
    }
}
