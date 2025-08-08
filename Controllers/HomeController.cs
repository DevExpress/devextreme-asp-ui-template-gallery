using DevExtreme.AspNet.Data;
using DevExtremeVSTemplateMVC.DAL;
using DevExtremeVSTemplateMVC.Models;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.AspNetCore.Mvc;

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
                    var tasks = _context.Tasks.Where(t => t.Status != "" && t.Owner == DemoConsts.DemoFilteredOwnerName).ToList();
                    var taskLists = _context.TaskLists.OrderBy(tl => tl.OrderIndex).ToList();

                    return View("../PlanningTasks/PlanningTasksKanban", new BoardViewModel
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

        [Route("Home/PlanningTasks")]
        public IActionResult PlanningTasks() {
            return Redirect("/Home/PlanningTasks/Grid" + Request.QueryString);
        }

        public IActionResult UserProfile()
        {
            return View("../CommonUserProfile/UserProfile", _context.Contacts.First());
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
