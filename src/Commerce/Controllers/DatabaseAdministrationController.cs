using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Commerce.Data;
using Commerce.Models;

namespace Commerce.Controllers
{
    public class DatabaseAdministrationController : Controller
    {
        private readonly CommerceContext _context;

        public DatabaseAdministrationController(CommerceContext context)
        {
            _context = context;
        }

        // GET: Test
        public async Task<IActionResult> Index()
        {
            return View(await _context.Financial_Transaction.ToListAsync());
        }

        // GET: Test/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financial_Transaction = await _context.Financial_Transaction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (financial_Transaction == null)
            {
                return NotFound();
            }

            return View(financial_Transaction);
        }

        // GET: Test/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountId,TimeStamp,Type,Amount,Balance_After,Description")] Financial_Transaction financial_Transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(financial_Transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(financial_Transaction);
        }

        // GET: Test/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financial_Transaction = await _context.Financial_Transaction.FindAsync(id);
            if (financial_Transaction == null)
            {
                return NotFound();
            }
            return View(financial_Transaction);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountId,TimeStamp,Type,Amount,Balance_After,Description")] Financial_Transaction financial_Transaction)
        {
            if (id != financial_Transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(financial_Transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestDBExists(financial_Transaction.Id))
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
            return View(financial_Transaction);
        }

        // GET: Test/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financial_Transaction = await _context.Financial_Transaction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (financial_Transaction == null)
            {
                return NotFound();
            }

            return View(financial_Transaction);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var financial_Transaction = await _context.Financial_Transaction.FindAsync(id);
            _context.Financial_Transaction.Remove(financial_Transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestDBExists(int id)
        {
            return _context.Financial_Transaction.Any(e => e.Id == id);
        }
    }
}
