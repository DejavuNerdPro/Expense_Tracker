using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerDotNetCoreMvcApp.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult AuthenticationIndex()
        {
            return View("AuthenticationIndex");
        }
    }
}
