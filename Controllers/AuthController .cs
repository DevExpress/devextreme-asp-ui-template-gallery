using Microsoft.AspNetCore.Mvc;
using DevExtremeVSTemplateMVC.DAL;

namespace DevExtremeVSTemplateMVC.Controllers
{
    public class AuthController : Controller {
        private readonly LocalDemoDataContext localDemoDataContext;

        public AuthController(LocalDemoDataContext localDemoDataContext) {
            this.localDemoDataContext = localDemoDataContext;
        }

        public IActionResult Login() {
            return View(localDemoDataContext.NewUser);
        }

        public IActionResult SignUp() {
            return View(localDemoDataContext.NewUser);
        }

        public IActionResult ForgotPassword() {
            return View(localDemoDataContext.NewUser);
        }
    }
}
