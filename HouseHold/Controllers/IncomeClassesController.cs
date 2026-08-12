using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HouseHold.Data;
using HouseHold.Models;

namespace HouseHold.Controllers
{
    public class IncomeClassesController : Controller
    {
        private readonly AppDbContext _context;

        public IncomeClassesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: IncomeClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.IncomeClasses.ToListAsync());
        }

        // GET: IncomeClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeClass = await _context.IncomeClasses
                .FirstOrDefaultAsync(m => m.IncomeClassId == id);
            if (incomeClass == null)
            {
                return NotFound();
            }

            return View(incomeClass);
        }

        // GET: IncomeClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IncomeClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IncomeClassId,IncomeName")] IncomeClass incomeClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incomeClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(incomeClass);
        }

        // GET: IncomeClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeClass = await _context.IncomeClasses.FindAsync(id);
            if (incomeClass == null)
            {
                return NotFound();
            }
            return View(incomeClass);
        }

        // POST: IncomeClasses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IncomeClassId,IncomeName")] IncomeClass incomeClass)
        {
            if (id != incomeClass.IncomeClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incomeClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomeClassExists(incomeClass.IncomeClassId))
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
            return View(incomeClass);
        }

        // GET: IncomeClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeClass = await _context.IncomeClasses
                .FirstOrDefaultAsync(m => m.IncomeClassId == id);
            if (incomeClass == null)
            {
                return NotFound();
            }

            return View(incomeClass);
        }

        // POST: IncomeClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incomeClass = await _context.IncomeClasses.FindAsync(id);
            if (incomeClass != null)
            {
                _context.IncomeClasses.Remove(incomeClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncomeClassExists(int id)
        {
            return _context.IncomeClasses.Any(e => e.IncomeClassId == id);
        }
    }
}
