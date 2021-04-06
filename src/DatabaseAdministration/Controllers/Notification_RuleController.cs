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
    public class Notification_RuleController : Controller
    {
        private readonly DbaContext _context;

        public Notification_RuleController(DbaContext context)
        {
            _context = context;
        }

        // GET: Notification_Rule
        public async Task<IActionResult> Index()
        {
            return View(await _context.Notification_Rule.ToListAsync());
        }

        // GET: Notification_Rule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification_Rule = await _context.Notification_Rule
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notification_Rule == null)
            {
                return NotFound();
            }

            return View(notification_Rule);
        }

        // GET: Notification_Rule/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notification_Rule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Customer_Id,Type,Condition,Value")] Notification_Rule notification_Rule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notification_Rule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notification_Rule);
        }

        // GET: Notification_Rule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification_Rule = await _context.Notification_Rule.FindAsync(id);
            if (notification_Rule == null)
            {
                return NotFound();
            }
            return View(notification_Rule);
        }

        // POST: Notification_Rule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Customer_Id,Type,Condition,Value")] Notification_Rule notification_Rule)
        {
            if (id != notification_Rule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notification_Rule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Notification_RuleExists(notification_Rule.Id))
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
            return View(notification_Rule);
        }

        // GET: Notification_Rule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification_Rule = await _context.Notification_Rule
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notification_Rule == null)
            {
                return NotFound();
            }

            return View(notification_Rule);
        }

        // POST: Notification_Rule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notification_Rule = await _context.Notification_Rule.FindAsync(id);
            _context.Notification_Rule.Remove(notification_Rule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Notification_RuleExists(int id)
        {
            return _context.Notification_Rule.Any(e => e.Id == id);
        }
    }
}
