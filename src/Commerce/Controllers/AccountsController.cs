using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commerce.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        /* TODO: 
         * 1. Connect to DB
         * 2. Fetch username & password
         * 3. IF: Valid, Login
         * 4. ELSE: Invalid, Error
         * */
        public IActionResult Verify()
        {
            return View();
        }
    }
}
