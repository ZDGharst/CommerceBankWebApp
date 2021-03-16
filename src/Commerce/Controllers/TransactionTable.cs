using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commerce.Controllers
{
    public class TransactionTable : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
