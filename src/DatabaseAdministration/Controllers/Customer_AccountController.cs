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
    public class Customer_AccountController : Controller
    {
        private readonly DbaContext _context;

        public Customer_AccountController(DbaContext context)
        {
            _context = context;
        }

        // GET: Customer_Account
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customer_Account.ToListAsync());
        }

        // GET: Customer_Account/Details/5
        public async Task<IActionResult> Details(string c_id, int a_id)
        {
            var customer_account = await _context.Customer_Account
                .FirstOrDefaultAsync(m => m.Customer_Id == c_id && m.Account_Id == a_id);
            if (customer_account == null)
            {
                return NotFound();
            }

            return View(customer_account);
        }

        // GET: Account/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Customer_Id,Account_Id")] Customer_Account customer_account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer_account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer_account);
        }

        // GET: Account/Delete/5
        public async Task<IActionResult> Delete(string c_id, int a_id)
        {
            var customer_account = await _context.Customer_Account
                .FirstOrDefaultAsync(m => m.Customer_Id == c_id && m.Account_Id == a_id);
            if (customer_account == null)
            {
                return NotFound();
            }

            return View(customer_account);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string c_id, int a_id)
        {
            var customer_account = _context.Customer_Account.Find(c_id, a_id);
            _context.Customer_Account.Remove(customer_account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
