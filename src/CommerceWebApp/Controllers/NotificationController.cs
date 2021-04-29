using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
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
                if ((notification_Rule.Type == "Deposit" || notification_Rule.Type == "Withdrawal") &&
                    (notification_Rule.Value <= 0))
                {
                    return View(notification_Rule);
                }

                if (notification_Rule.Type == "Login")
                {
                    notification_Rule.Condition = "NA";
                    notification_Rule.Value = 0;
                }

                if (notification_Rule.Message == null || notification_Rule.Message.ToString() == "")
                {
                    notification_Rule.Message = "NA";
                }

                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@customer_id",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size = 450,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = User.FindFirstValue(ClaimTypes.NameIdentifier)
                        },
                        new SqlParameter() {
                            ParameterName = "@type",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 32,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = notification_Rule.Type
                        },
                        new SqlParameter() {
                            ParameterName = "@condition",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 32,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = notification_Rule.Condition
                        },
                        new SqlParameter() {
                            ParameterName = "@value",
                            SqlDbType =  System.Data.SqlDbType.Decimal,
                            /*Precision = 18,
                            Scale = 2,*/
                            Direction = System.Data.ParameterDirection.Input,
                            Value = notification_Rule.Value
                        },
                        new SqlParameter() {
                            ParameterName = "@notify_text",
                            SqlDbType =  System.Data.SqlDbType.Bit,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = notification_Rule.Notify_Text
                        },
                        new SqlParameter() {
                            ParameterName = "@notify_email",
                            SqlDbType =  System.Data.SqlDbType.Bit,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = notification_Rule.Notify_Email
                        },
                        new SqlParameter() {
                            ParameterName = "@notify_web",
                            SqlDbType =  System.Data.SqlDbType.Bit,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = notification_Rule.Notify_Web
                        },
                        new SqlParameter() {
                            ParameterName = "@message",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 300,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = notification_Rule.Message
                        }};

                await _context.Database.ExecuteSqlRawAsync("[dbo].[AddNotificationRule] @customer_id, @type, @condition, @value, @notify_text, @notify_email, @notify_web, @message", param);
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
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@id",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = id
                        }
            };

            await _context.Database.ExecuteSqlRawAsync("[dbo].[DelNotificationRule] @id", param);
            await _context.SaveChangesAsync();
            return RedirectToAction("Manage");
        }

        public async Task<IActionResult> Edit(int? id)
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

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(Notification_Rule notification_Rule, int id)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@id",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = id
                        },
                        new SqlParameter() {
                            ParameterName = "@customer_id",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size = 450,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = User.FindFirstValue(ClaimTypes.NameIdentifier)
                        },
                        new SqlParameter() {
                            ParameterName = "@type",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 32,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = notification_Rule.Type
                        },
                        new SqlParameter() {
                            ParameterName = "@condition",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 32,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = notification_Rule.Condition
                        },
                        new SqlParameter() {
                            ParameterName = "@value",
                            SqlDbType =  System.Data.SqlDbType.Decimal,
                            /*Precision = 18,
                            Scale = 2,*/
                            Direction = System.Data.ParameterDirection.Input,
                            Value = notification_Rule.Value
                        },
                        new SqlParameter() {
                            ParameterName = "@notify_text",
                            SqlDbType =  System.Data.SqlDbType.Bit,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = notification_Rule.Notify_Text
                        },
                        new SqlParameter() {
                            ParameterName = "@notify_email",
                            SqlDbType =  System.Data.SqlDbType.Bit,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = notification_Rule.Notify_Email
                        },
                        new SqlParameter() {
                            ParameterName = "@notify_web",
                            SqlDbType =  System.Data.SqlDbType.Bit,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = notification_Rule.Notify_Web
                        },
                        new SqlParameter() {
                            ParameterName = "@message",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 300,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = notification_Rule.Message
                        }};

            await _context.Database.ExecuteSqlRawAsync("[dbo].[EditNotificationRule] @id, @customer_id, @type, @condition, @value, @notify_text, @notify_email, @notify_web, @message", param);
            await _context.SaveChangesAsync();
            return RedirectToAction("Manage");
        }
    }
}
