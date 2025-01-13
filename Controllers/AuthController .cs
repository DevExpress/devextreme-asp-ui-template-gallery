using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using devextreme_asp_ui_template_gallery.Models;

namespace DevExtremeVSTemplateMVC.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login() {
            return View(new UserData());
        }

        public IActionResult SignUp() {
            return View(new UserData());
        }

        public IActionResult ForgotPassword() {
            return View(new UserData());
        }
    }
}
