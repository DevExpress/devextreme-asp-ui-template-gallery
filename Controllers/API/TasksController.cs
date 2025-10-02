using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DevExtremeVSTemplateMVC.DAL;
using DevExtremeVSTemplateMVC.Models;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevExtremeVSTemplateMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly DemoDbContext _context;

        public TasksController(DemoDbContext context, IHttpContextAccessor accessor) {
            _context = context;
        }
        
        [HttpGet]
        public object GetTasks(DataSourceLoadOptions loadOptions) {
            return DataSourceLoader.Load(_context.Tasks, loadOptions);
        }

        [HttpPost]
        public IActionResult InsertTask([FromForm] string values) {
            EmployeeTask employeeTask = new EmployeeTask();
            UpdateTaskProperties(employeeTask, values);
            //employeeTask.TaskId = _context.Tasks.Where(t => t.Owner == employeeTask.Owner).Max(t => t.TaskId) + 1;
            _context.Tasks.Add(employeeTask);
            _context.SaveChanges();
            return Json(new { employeeTask.Id });
        }

        [HttpDelete("DeleteTask")]
        public object DeleteTask([FromForm] int key) {
            var task = _context.Tasks.FirstOrDefault(item => item.Id == key);
            if (task == null) return NotFound();
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("DeleteFilteredTask")]
        public object DeleteFilteredTask([FromForm] int key) {
            var task = _context.Tasks.FirstOrDefault(item => item.TaskId == key && item.Owner == DemoConsts.DemoFilteredOwnerName);
            if (task == null) return NotFound();
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("UpdateTask")]
        public IActionResult UpdateTask([FromForm] int key, [FromForm] string values) {
            EmployeeTask task = _context.Tasks.FirstOrDefault(t => t.Id == key);
            if (task == null) return NotFound();
            return UpdateTaskProperties(task, values);
        }

        [HttpPut("UpdateFilteredTask")]
        public IActionResult UpdateFilteredTask([FromForm] int key, [FromForm] string values) {
            EmployeeTask task = _context.Tasks.FirstOrDefault(t => t.Owner == DemoConsts.DemoFilteredOwnerName && t.TaskId == key);
            if (task == null) return NotFound();
            return UpdateTaskProperties(task, values);
        }

        IActionResult UpdateTaskProperties(EmployeeTask task, string values) {
            var updatedValues = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(values);
            PopulateModel(task, updatedValues);
            if (!TryValidateModel(task)) {
                string[] errors = ModelState
                    .Where(kvp => kvp.Value.Errors.Count > 0)
                    .SelectMany(kvp => kvp.Value.Errors)
                    .Select(e => e.ErrorMessage).ToArray();
                return BadRequest(String.Join(Environment.NewLine, errors));
            }
            AdjustOrderIndices(task);
            _context.SaveChanges();
            return Ok();
        }

        void AdjustOrderIndices(EmployeeTask task) {
            var tasksWithStatus = _context.Tasks
                .Where(t => t.Status == task.Status && t.Owner == DemoConsts.DemoFilteredOwnerName && t.Id != task.Id)
                .OrderBy(t => t.OrderIndex)
                .ToList();
            tasksWithStatus.Insert(Math.Min(task.OrderIndex, tasksWithStatus.Count), task);
            for (int i = 0; i < tasksWithStatus.Count; i++)
                tasksWithStatus[i].OrderIndex = i;
        }

        void PopulateModel(EmployeeTask task, Dictionary<string, JsonElement> updatedValues) {
            var entry = _context.Entry(task);
            foreach (var kv in updatedValues) {
                var propertyName = kv.Key;
                var property = entry.Metadata.FindProperty(propertyName);
                if (property == null || property.IsPrimaryKey())
                    continue;
                var typedValue = JsonSerializer.Deserialize(kv.Value.GetRawText(), property.ClrType);
                entry.CurrentValues[propertyName] = typedValue;
            }
        }
    }
}
