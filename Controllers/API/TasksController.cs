using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DevExtremeVSTemplateMVC.DAL;
using DevExtremeVSTemplateMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text.Json;

namespace DevExtremeVSTemplateMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly RwaContext _context;

        public TasksController(RwaContext context, IHttpContextAccessor accessor) {
            _context = context;
        }

        [HttpGet]
        public object GetTasks(DataSourceLoadOptions loadOptions) {
            return DataSourceLoader.Load(_context.Tasks, loadOptions);
        }

        [HttpPut]
        public IActionResult UpdateTask([FromForm] int key, [FromForm] string values) {
            EmployeeTask task = _context.Tasks.FirstOrDefault(t => t.TaskId == key);
            if (task == null) return NotFound();
            var updatedValues = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(values);
            PopulateModel(task, updatedValues);
            if (!TryValidateModel(task))
                return BadRequest("Validation Failed");

            _context.SaveChanges();
            return Ok();
        }

        void PopulateModel(EmployeeTask task, Dictionary<string, JsonElement> updatedValues) {
            //_context.Entry(task).CurrentValues.SetValues(updatedValues);
            foreach (var entry in updatedValues) {
                var property = typeof(EmployeeTask).GetProperty(entry.Key);
                if (property != null) {
                    var value = JsonSerializer.Deserialize(entry.Value.GetRawText(), property.PropertyType);
                    property.SetValue(task, value);
                }
            }
        }
    }
}
