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
        const string OWNER_NAME_TO_FILTER = "Sammy Hill";

        public TasksController(RwaContext context, IHttpContextAccessor accessor) {
            _context = context;
        }
        
        [HttpGet]
        public object GetTasks(DataSourceLoadOptions loadOptions) {
            return DataSourceLoader.Load(_context.Tasks, loadOptions);
        }

        [HttpGet("GetFilteredTasks")]
        public object GetFilteredTasks(DataSourceLoadOptions loadOptions) {
            var filteredTasks = _context.Tasks.Where(t => t.Owner == OWNER_NAME_TO_FILTER);
            return DataSourceLoader.Load(filteredTasks, loadOptions);
        }

        [HttpPut("UpdateTask")]
        public IActionResult UpdateTask([FromForm] int key, [FromForm] string values) {
            EmployeeTask task = _context.Tasks.FirstOrDefault(t => t.TaskId == key);
            return UpdateTaskProperties(task, values);
        }

        [HttpPut("UpdateFilteredTask")]
        public IActionResult UpdateFilteredTask([FromForm] int key, [FromForm] string values) {
            EmployeeTask task = _context.Tasks.FirstOrDefault(t => t.Owner == OWNER_NAME_TO_FILTER && t.Id == key);
            return UpdateTaskProperties(task, values);
        }

        IActionResult UpdateTaskProperties(EmployeeTask task, string values) {
            if (task == null) return NotFound();
            var updatedValues = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(values);
            PopulateModel(task, updatedValues);
            if (!TryValidateModel(task))
                return BadRequest("Validation Failed");

            _context.SaveChanges();
            return Ok();
        }

        void PopulateModel(EmployeeTask task, Dictionary<string, JsonElement> updatedValues) {
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
