using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;

using Commerce_WebApp.Models;
using Commerce_WebApp.Data;

namespace Commerce_WebApp.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Manage()
        {
            return View(await _context.Notification_Rule.FromSqlInterpolated($"ReturnNotificationRules {User.Identity.Name}").ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notification_Rule notification_Rule)
        {
            notification_Rule.Customer_Id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if ((ModelState.IsValid) &&
                (notification_Rule.Type == "Login" || notification_Rule.Type == "Balance" || notification_Rule.Type == "Deposit" || notification_Rule.Type == "Withdrawal") &&
                (notification_Rule.Condition == "Above" || notification_Rule.Condition == "Below" || notification_Rule.Condition == "NA") &&
                (notification_Rule.Value >= 0))
            {
                /*SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddNotificationRule";
                cmd.Parameters.AddWithValue("@customer_id", User.FindFirstValue(ClaimTypes.NameIdentifier));
                cmd.Parameters.AddWithValue("@type", notification_Rule.Type);
                cmd.Parameters.AddWithValue("@condition", notification_Rule.Condition);
                cmd.Parameters.AddWithValue("@value", notification_Rule.Value);
                cmd.Parameters.AddWithValue("@notify_text", notification_Rule.Notify_Text);
                cmd.Parameters.AddWithValue("@notify_email", notification_Rule.Notify_Email);
                cmd.Parameters.AddWithValue("@notify_web", notification_Rule.Notify_Web);
                cmd.Parameters.AddWithValue("@message", notification_Rule.Message);*/

                if (notification_Rule.Type == "Login")
                {
                    notification_Rule.Condition = "NA";
                    notification_Rule.Value = 0;
                }

                if ((notification_Rule.Type == "Deposit" || notification_Rule.Type == "Withdrawal") &&
                    (notification_Rule.Value <= 0))
                {
                    return View(notification_Rule);
                }

                _context.Add(notification_Rule);
                await _context.SaveChangesAsync();
                return RedirectToAction("Manage");
            }

            return View(notification_Rule);
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notification_Rule = await _context.Notification_Rule.FindAsync(id);
            _context.Notification_Rule.Remove(notification_Rule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
