using Microsoft.AspNetCore.Mvc;

namespace devextreme_asp_ui_template_gallery.ViewComponents
{
    public class AboutViewComponent : ViewComponent
    {
        public AboutViewComponent() {
        }
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
