using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DevExtremeVSTemplateMVC.DAL;
using DevExtremeVSTemplateMVC.Models;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;
using System.Text.Json;

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

        [HttpPut("UpdateTask")]
        public IActionResult UpdateTask([FromForm] int key, [FromForm] string values) {
            EmployeeTask task = _context.Tasks.FirstOrDefault(t => t.TaskId == key);
            if (task == null) return NotFound();
            return UpdateTaskProperties(task, values);
        }

        [HttpPut("UpdateFilteredTask")]
        public IActionResult UpdateFilteredTask([FromForm] int key, [FromForm] string values) {
            EmployeeTask task = _context.Tasks.FirstOrDefault(t => t.Owner == DemoConsts.DemoFilteredOwnerName && t.Id == key);
            if (task == null) return NotFound();
            return UpdateTaskProperties(task, values);
        }

        IActionResult UpdateTaskProperties(EmployeeTask task, string values) {
            var updatedValues = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(values);

            int? newOrderIndex = null;
            if (updatedValues.ContainsKey("NewOrderIndex"))
            {
                newOrderIndex = updatedValues["NewOrderIndex"].GetInt32();
                updatedValues.Remove("NewOrderIndex");
            }

            PopulateModel(task, updatedValues);
            if (!TryValidateModel(task)) {
                string[] errors = ModelState
                    .Where(kvp => kvp.Value.Errors.Count > 0)
                    .SelectMany(kvp => kvp.Value.Errors)
                    .Select(e => e.ErrorMessage).ToArray();
                return BadRequest(String.Join(Environment.NewLine, errors));
            }

            if (newOrderIndex.HasValue)
            {
                var status = task.Status;
                var tasksWithSameStatus = _context.Tasks
                    .Where(t => t.Status == status && t.TaskId != task.TaskId && t.OrderIndex >= newOrderIndex.Value-1)
                    .OrderByDescending(t => t.OrderIndex)
                    .ToList();

                foreach (var t in tasksWithSameStatus)
                {
                    t.OrderIndex += 1;
                }

                tasksWithSameStatus.Where(t => t.TaskId == task.TaskId).ToList().ForEach(t => t.OrderIndex = newOrderIndex.Value-1);
            }

            _context.SaveChanges();

            Debug.WriteLine(
                "Saved: "+string.Join(
                    ", ",
                    _context.Tasks
                        .Where(t => t.Status == "Deferred")
                        .Select(t => $"TaskId: {t.TaskId}, Text: {t.Text}, Status: {t.Status}")
                )
            );

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
