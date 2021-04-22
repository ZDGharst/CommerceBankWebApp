using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Commerce_WebApp.Models;
using Commerce_WebApp.Data;

namespace Commerce_WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return View("RequireLogin");
            }

            var Customer_Account = await _context.Customer_Account.FirstOrDefaultAsync(m => m.Customer_Id == userId && m.Account_Id == id);
            if (id == null || Customer_Account == null)
            {
                return View(await _context.Account.FromSqlInterpolated($"ReturnAccounts {userId}").ToListAsync());
            }

            return View("Account", await _context.Financial_Transaction.FromSqlInterpolated($"ReturnTransactions {id}").ToListAsync());
        }
    }
}