using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DevExtremeVSTemplateMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About() {
            return View();
        }

        public IActionResult PlanningTasks() {
            return View("../PlanningTasks/PlanningTasks");
        }

        public IActionResult Login() {
            return View("../Auth/Login");
        }

        public IActionResult SignUp() {
            return View("../Auth/SignUp");
        }

        public IActionResult ForgotPassword() {
            return View("../Auth/ForgotPassword");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View();
        }
    }
}
