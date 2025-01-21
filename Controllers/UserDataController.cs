using DevExtremeVSTemplateMVC.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace DevExtremeVSTemplateMVC.Controllers
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
