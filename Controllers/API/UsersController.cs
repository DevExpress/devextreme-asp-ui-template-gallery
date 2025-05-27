using DevExtremeVSTemplateMVC.DAL;
using Microsoft.AspNetCore.Mvc;

namespace DevExtremeVSTemplateMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController {
        private readonly DemoDbContext _context;

        public UsersController(DemoDbContext context) {
            _context = context;
        }

        [HttpGet("Contacts/{id}")]
        public object GetContact(int id) {
            return _context.Contacts.FirstOrDefault(c => c.Id == id);
        }
    }
}
