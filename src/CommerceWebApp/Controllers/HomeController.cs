using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Text;
using Commerce_WebApp.Models;
using Commerce_WebApp.Data;

namespace Commerce_WebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public HomeController(SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Index(string? timePeriod)
        {
            ViewBag.timePeriod = timePeriod;

            if(!_signInManager.IsSignedIn(User))
            {
                return new RedirectToPageResult("/Account/Login", new { area = "Identity" });
            }

            var timeDiff = -1;

            if(timePeriod == "yearly")
            {
                timeDiff = -12;
            }
            else if(timePeriod == "all")
            {
                timeDiff = -600;
            }

            DateTime time = DateTime.Now;
            time = time.AddMonths(timeDiff);

            return View(await _context.Notification_Triggered.FromSqlInterpolated($"ReturnTriggeredNotifications {User.Identity.Name}, {time}").ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public FileResult ExportAll()
        {

            var timeDiff = -600;

            DateTime time = DateTime.Now;
            time = time.AddMonths(timeDiff);

            List<Notification_Triggered> notification_Triggered = _context.Notification_Triggered.FromSqlInterpolated($"ReturnTriggeredNotifications {User.Identity.Name}, {time}").ToList<Notification_Triggered>();
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Type" + ',');
            stringBuilder.Append("Condition" + ',');
            stringBuilder.Append("Value" + ',');
            stringBuilder.Append("Count" + ',');
            stringBuilder.Append("\r\n");

            foreach (var notification in notification_Triggered)
            {
                if (notification.Type == "Login")
                {
                    stringBuilder.Append(notification.Type + ',');
                    stringBuilder.Append("---" + ',');
                    stringBuilder.Append("---" + ',');
                    stringBuilder.Append(notification.Count.ToString() + ',');
                }

                else
                {
                    stringBuilder.Append(notification.Type + ',');
                    stringBuilder.Append(notification.Condition + ',');
                    stringBuilder.Append(notification.Value.ToString() + ',');
                    stringBuilder.Append(notification.Count.ToString() + ',');
                }

                stringBuilder.Append("\r\n");
            }

            return File(Encoding.UTF8.GetBytes(stringBuilder.ToString()), "text/csv", "Notification_Log_All.csv");
        }

        [HttpPost]
        public FileResult ExportMonth()
        {
            var timeDiff = -1;

            DateTime time = DateTime.Now;
            time = time.AddMonths(timeDiff);

            List<Notification_Triggered> notification_Triggered = _context.Notification_Triggered.FromSqlInterpolated($"ReturnTriggeredNotifications {User.Identity.Name}, {time}").ToList<Notification_Triggered>();
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Type" + ',');
            stringBuilder.Append("Condition" + ',');
            stringBuilder.Append("Value" + ',');
            stringBuilder.Append("Count" + ',');
            stringBuilder.Append("\r\n");

            foreach (var notification in notification_Triggered)
            {
                if (notification.Type == "Login")
                {
                    stringBuilder.Append(notification.Type + ',');
                    stringBuilder.Append("---" + ',');
                    stringBuilder.Append("---" + ',');
                    stringBuilder.Append(notification.Count.ToString() + ',');
                }

                else
                {
                    stringBuilder.Append(notification.Type + ',');
                    stringBuilder.Append(notification.Condition + ',');
                    stringBuilder.Append(notification.Value.ToString() + ',');
                    stringBuilder.Append(notification.Count.ToString() + ',');
                }

                stringBuilder.Append("\r\n");
            }

            return File(Encoding.UTF8.GetBytes(stringBuilder.ToString()), "text/csv", "Notification_Log_Monthly.csv");
        }

        [HttpPost]
        public FileResult ExportYear()
        {
            var timeDiff = -12;

            DateTime time = DateTime.Now;
            time = time.AddMonths(timeDiff);

            List<Notification_Triggered> notification_Triggered = _context.Notification_Triggered.FromSqlInterpolated($"ReturnTriggeredNotifications {User.Identity.Name}, {time}").ToList<Notification_Triggered>();
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Type" + ',');
            stringBuilder.Append("Condition" + ',');
            stringBuilder.Append("Value" + ',');
            stringBuilder.Append("Count" + ',');
            stringBuilder.Append("\r\n");

            foreach (var notification in notification_Triggered)
            {
                if (notification.Type == "Login")
                {
                    stringBuilder.Append(notification.Type + ',');
                    stringBuilder.Append("---" + ',');
                    stringBuilder.Append("---" + ',');
                    stringBuilder.Append(notification.Count.ToString() + ',');
                }

                else
                {
                    stringBuilder.Append(notification.Type + ',');
                    stringBuilder.Append(notification.Condition + ',');
                    stringBuilder.Append(notification.Value.ToString() + ',');
                    stringBuilder.Append(notification.Count.ToString() + ',');
                }

                stringBuilder.Append("\r\n");
            }

            return File(Encoding.UTF8.GetBytes(stringBuilder.ToString()), "text/csv", "Notification_Log_Yearly.csv");
        }
    }
}
