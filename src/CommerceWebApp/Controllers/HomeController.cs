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

        public IActionResult Index()
        {
            if(!_signInManager.IsSignedIn(User))
            {
                return new RedirectToPageResult("/Account/Login", new { area = "Identity" });
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public FileResult Export()
        {
            /*List<object> notification_Rules = _context.Notification_Rule.FromSqlInterpolated($"ReturnNotificationRules {User.Identity.Name}").ToList<object>();
            notification_Rules.Insert(0, new string[4] { "1", "2", "3", "4" });

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < notification_Rules.Count; i++)
            {
                string[] rule = (string[])notification_Rules[i];
                for (int j = 0; j < rule.Length; j++)
                {
                    stringBuilder.Append(rule[j] + ',');
                }

                stringBuilder.Append("\r\n");
            }

            return File(Encoding.UTF8.GetBytes(stringBuilder.ToString()), "text/csv", "Grid.csv");*/

            List<object> notification_rules = (from rule in _context.Notification_Rule.ToList().Take(10) select new[] { rule.Value.ToString(), rule.Type, rule.Condition }).ToList<object>();

            notification_rules.Insert(0, new string[3] { "Value", "Type", "Condition" });

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < notification_rules.Count; i++)
            {
                string[] rule = (string[])notification_rules[i];
                for (int j = 0; j < rule.Length; j++)
                {
                    //Append data with separator.
                    if (rule[j] == "NA")
                    {
                        stringBuilder.Append("---" + ',');
                    }

                    else
                    {
                        stringBuilder.Append(rule[j] + ',');
                    }
                }

                //Append new line character.
                stringBuilder.Append("\r\n");

            }

            return File(Encoding.UTF8.GetBytes(stringBuilder.ToString()), "text/csv", "Grid.csv");
        }
    }
}
