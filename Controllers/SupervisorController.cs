using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DevExtremeVSTemplateMVC.DAL;
using DevExtremeVSTemplateMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevExtremeVSTemplateMVC.Controllers
{
    [Route("api/[controller]")]
    public class SupervisorController : Controller {
        private readonly LocalDemoDataContext localDemoContext;

        public SupervisorController(LocalDemoDataContext localDemoContext) {
            this.localDemoContext = localDemoContext;
        }

        [HttpGet]
        public object Get() {
            return localDemoContext.Supervisors;
        }
    }
}
