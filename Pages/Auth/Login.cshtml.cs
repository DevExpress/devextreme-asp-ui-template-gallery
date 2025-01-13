using DevExpress.Internal;
using devextreme_asp_ui_template_gallery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using UserData = devextreme_asp_ui_template_gallery.Models.UserData;

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
