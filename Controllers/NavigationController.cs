using Microsoft.AspNetCore.Mvc;
using DevExtreme.Asp.Template.Gallery.Models.Routing;

namespace DevExtreme.Asp.Template.Gallery.Controllers
{
    public class NavigationController : Controller
    {
        public object GetNavigationData() {
            return RoutingData.Routes;
        }
        public IActionResult GetComponent(string componentName) {
            return ViewComponent(componentName);
        }
    }
}
