using Microsoft.AspNetCore.Mvc;

namespace devextreme_asp_ui_template_gallery.ViewComponents
{
    public class TasksViewComponent : ViewComponent
    {
        public TasksViewComponent() {
        }
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
