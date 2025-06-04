using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtremeVSTemplateMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DevExtremeVSTemplateMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("PlanningTasks", "Home");
        }

        public IActionResult PlanningTasks() {
            return View("../PlanningTasks/PlanningTasks");
        }

        public IActionResult UserProfile()
        {
            return View("../CommonUserProfile/UserProfile", ProfileData.Profiles[0]);
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

        [HttpPost]
        public IActionResult TaskMainSortable([FromForm] string filteredTasks)
        {
            List<TaskModel> model = JsonConvert.DeserializeObject<List<TaskModel>>(filteredTasks);
            return PartialView("../PlanningTasks/Kanban/_TaskMainSortable", model);
        }

        #region Partial Views
        public ActionResult GetPlanningTasksGrid() {
            return PartialView("../PlanningTasks/_PlanningTasksGrid");
        }
        public ActionResult GetPlanningTasksGantt() {
            return PartialView("../PlanningTasks/_PlanningTasksGantt");
        }
        public ActionResult GetPlanningTasksKanban() {
            return PartialView("../PlanningTasks/_PlanningTasksKanban");
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View();
        }
    }
}
