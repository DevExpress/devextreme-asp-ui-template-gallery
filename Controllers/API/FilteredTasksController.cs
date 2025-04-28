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
    public class FilteredTasksController : Controller
    {
        private readonly RwaContext _context;

        public FilteredTasksController(RwaContext context) {
            _context = context;
        }

        [HttpGet]
        public object GetTasks(DataSourceLoadOptions loadOptions) {
            return DataSourceLoader.Load(_context.FilteredTasks, loadOptions);
        }

        [HttpPut]
        public IActionResult UpdateTask([FromForm] int key, [FromForm] string values) {
            FilteredTask task = _context.FilteredTasks.FirstOrDefault(t => t.Id == key);
            if (task == null) return NotFound();
            var updatedValues = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(values);
            PopulateModel(task, updatedValues);
            if (!TryValidateModel(task))
                return BadRequest("Validation Failed");

            _context.SaveChanges();
            return Ok();
        }

        void PopulateModel(FilteredTask task, Dictionary<string, JsonElement> updatedValues) {
            foreach (var entry in updatedValues) {
                var property = typeof(FilteredTask).GetProperty(entry.Key);
                if (property != null) {
                    var value = JsonSerializer.Deserialize(entry.Value.GetRawText(), property.PropertyType);
                    property.SetValue(task, value);
                }
            }
        }
    }
}
