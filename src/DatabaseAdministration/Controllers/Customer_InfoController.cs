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
    public class Customer_InfoController : Controller
    {
        private readonly DbaContext _context;

        public Customer_InfoController(DbaContext context)
        {
            _context = context;
        }

        // GET: Customer_Info
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customer_Info.ToListAsync());
        }

        // GET: Customer_Info/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer_Info = await _context.Customer_Info
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer_Info == null)
            {
                return NotFound();
            }

            return View(customer_Info);
        }

        // GET: Customer_Info/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer_Info/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,First_Name,Last_Name,Date_Of_Birth,Address,City,State,Zip")] Customer_Info customer_Info)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer_Info);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer_Info);
        }

        // GET: Customer_Info/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer_Info = await _context.Customer_Info.FindAsync(id);
            if (customer_Info == null)
            {
                return NotFound();
            }
            return View(customer_Info);
        }

        // POST: Customer_Info/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,First_Name,Last_Name,Date_Of_Birth,Address,City,State,Zip")] Customer_Info customer_Info)
        {
            if (id != customer_Info.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer_Info);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Customer_InfoExists(customer_Info.Id))
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
            return View(customer_Info);
        }

        // GET: Customer_Info/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer_Info = await _context.Customer_Info
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer_Info == null)
            {
                return NotFound();
            }

            return View(customer_Info);
        }

        // POST: Customer_Info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customer_Info = await _context.Customer_Info.FindAsync(id);
            _context.Customer_Info.Remove(customer_Info);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Customer_InfoExists(string id)
        {
            return _context.Customer_Info.Any(e => e.Id == id);
        }
    }
}
