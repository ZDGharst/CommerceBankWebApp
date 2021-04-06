using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseAdministration.Data;
using DatabaseAdministration.Models;

namespace DatabaseAdministration.Controllers
{
    public class Financial_TransactionController : Controller
    {
        private readonly DbaContext _context;

        public Financial_TransactionController(DbaContext context)
        {
            _context = context;
        }

        // GET: Financial_Transaction
        public async Task<IActionResult> Index()
        {
            return View(await _context.Financial_Transaction.ToListAsync());
        }

        // GET: Financial_Transaction/Details/5
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

        // GET: Financial_Transaction/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Financial_Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Account_Id,TimeStamp,Type,Description,Amount,Balance_After")] Financial_Transaction financial_Transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(financial_Transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(financial_Transaction);
        }

        // GET: Financial_Transaction/Edit/5
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

        // POST: Financial_Transaction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Account_Id,TimeStamp,Type,Description,Amount,Balance_After")] Financial_Transaction financial_Transaction)
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
                    if (!Financial_TransactionExists(financial_Transaction.Id))
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

        // GET: Financial_Transaction/Delete/5
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

        // POST: Financial_Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var financial_Transaction = await _context.Financial_Transaction.FindAsync(id);
            _context.Financial_Transaction.Remove(financial_Transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Financial_TransactionExists(int id)
        {
            return _context.Financial_Transaction.Any(e => e.Id == id);
        }
    }
}
