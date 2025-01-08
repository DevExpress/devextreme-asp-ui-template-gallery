using Microsoft.AspNetCore.Mvc;

namespace devextreme_asp_ui_template_gallery.Controllers
{
    public class ComponentsController : Controller {
        public IActionResult GetTasksComponent() {
            return ViewComponent("Tasks");
        }
        public IActionResult GetAboutComponent() {
            return ViewComponent("About");
        }
    }
}
