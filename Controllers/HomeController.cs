using DevExtreme.AspNet.Data;
using DevExtremeVSTemplateMVC.DAL;
using DevExtremeVSTemplateMVC.Models;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DevExtremeVSTemplateMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly DemoDbContext _context;

        public HomeController(DemoDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return Redirect("Home/PlanningTasks/Grid");
        }

        public IActionResult About() {
            return View();
        }

        [Route("Home/PlanningTasks/{viewType}")]
        public IActionResult PlanningTasks(string viewType) {
            switch (viewType.ToLower()) {
                case "grid":
                    return View("../PlanningTasks/PlanningTasksGrid");
                case "kanban":
                    var tasks = _context.Tasks.Where(t => t.Status != "" && t.Owner != null).ToList();
                    var taskLists = _context.TaskLists.OrderBy(tl => tl.OrderIndex).ToList();

                    return View("../PlanningTasks/PlanningTasksKanban", new TaskMainSortableViewModel
                    {
                        AllTasks = tasks,
                        BoardLists = taskLists
                    });
                case "gantt":
                    return View("../PlanningTasks/PlanningTasksGantt");
                case "":
                    return View("../PlanningTasks/PlanningTasksGrid");
                default:
                    return NotFound();
            }
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

        #region Partial Views
        public ActionResult GetPlanningTasksGrid() {
            return PartialView("../PlanningTasks/_PlanningTasksGrid");
        }
        public ActionResult GetPlanningTasksGantt() {
            return PartialView("../PlanningTasks/_PlanningTasksGantt");
        }
        public ActionResult GetPlanningTasksKanban() {
            var tasks = _context.Tasks.Where(t => t.Status != "").ToList();
            var taskLists = _context.TaskLists.OrderBy(tl => tl.OrderIndex).ToList();

            return PartialView("../PlanningTasks/_PlanningTasksKanban", new TaskMainSortableViewModel
            {
                AllTasks = tasks,
                BoardLists = taskLists
            });
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View();
        }
    }
}
