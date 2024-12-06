using DevExpress.Internal;
using DevExtreme.Asp.Template.Gallery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using UserData = DevExtreme.Asp.Template.Gallery.Models.UserData;

namespace DevExtreme.Asp.Template.Gallery.Pages.Auth
{
    public class LoginModel : PageModel
    {
        public UserData ClientAuth = new UserData();
        public void OnGet()
        {
            ClientAuth = SampleUser.User;
        }
    }
}
