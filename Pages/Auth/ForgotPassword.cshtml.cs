using DevExtreme.Asp.Template.Gallery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevExtreme.Asp.Template.Gallery.Pages.Auth
{
    public class ForgotPasswordModel : PageModel
    {
        public UserData ClientAuth = new UserData();
        public void OnGet()
        {
            ClientAuth = SampleUser.User;
        }
    }
}
