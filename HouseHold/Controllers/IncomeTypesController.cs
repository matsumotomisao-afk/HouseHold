using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HouseHold.Data;
using HouseHold.Models;

namespace HouseHold.Controllers
{
    public class IncomeTypesController : Controller
    {
        private readonly AppDbContext _context;

        public IncomeTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: IncomeTypes
        public async Task<IActionResult> Index()
        {
            var incomeTypes = await _context.IncomeTypes
                .Include(i => i.IncomeClass)
                .ToListAsync();
            return View(incomeTypes);
        }

        // GET: IncomeTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeType = await _context.IncomeTypes
                .Include(i => i.IncomeClass)
                .FirstOrDefaultAsync(m => m.IncomeTypeId == id);
            if (incomeType == null)
            {
                return NotFound();
            }

            return View(incomeType);
        }

        // GET: IncomeTypes/Create
        public IActionResult Create()
        {
            ViewData["IncomeClassId"] = new SelectList(_context.IncomeClasses, "IncomeClassId", "IncomeName");
            return View();
        }

        // POST: IncomeTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IncomeTypeId,IncomeClassId,TypeName")] IncomeType incomeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incomeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IncomeClassId"] = new SelectList(_context.IncomeClasses, "IncomeClassId", "IncomeName", incomeType.IncomeClassId);
            return View(incomeType);
        }

        // GET: IncomeTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeType = await _context.IncomeTypes.FindAsync(id);
            if (incomeType == null)
            {
                return NotFound();
            }
            ViewData["IncomeClassId"] = new SelectList(_context.IncomeClasses, "IncomeClassId", "IncomeName", incomeType.IncomeClassId);
            return View(incomeType);
        }

        // POST: IncomeTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IncomeTypeId,IncomeClassId,TypeName")] IncomeType incomeType)
        {
            if (id != incomeType.IncomeTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incomeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomeTypeExists(incomeType.IncomeTypeId))
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
            ViewData["IncomeClassId"] = new SelectList(_context.IncomeClasses, "IncomeClassId", "IncomeName", incomeType.IncomeClassId);
            return View(incomeType);
        }

        // GET: IncomeTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeType = await _context.IncomeTypes
                .Include(i => i.IncomeClass)
                .FirstOrDefaultAsync(m => m.IncomeTypeId == id);
            if (incomeType == null)
            {
                return NotFound();
            }

            return View(incomeType);
        }

        // POST: IncomeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incomeType = await _context.IncomeTypes.FindAsync(id);
            if (incomeType != null)
            {
                _context.IncomeTypes.Remove(incomeType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncomeTypeExists(int id)
        {
            return _context.IncomeTypes.Any(e => e.IncomeTypeId == id);
        }
    }
}
