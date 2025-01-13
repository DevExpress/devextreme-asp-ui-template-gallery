using devextreme_asp_ui_template_gallery.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace DevExtreme.Asp.Template.Gallery.Controllers
{
    [Route("api/[controller]")]
    public class UserDataController : Controller
    {
        [HttpGet]
        public object Get()
        {
            return Ok(new { 
                url = SampleUser.User.AvatarUrl,
                name = SampleUser.User.Name,
                lastName = SampleUser.User.LastName });
        }
    }
}
