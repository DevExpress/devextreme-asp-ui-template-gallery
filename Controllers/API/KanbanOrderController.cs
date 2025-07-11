using DevExtremeVSTemplateMVC.DAL;
using DevExtremeVSTemplateMVC.Models;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace DevExtremeVSTemplateMVC.Controllers { 

    [ApiController]
    [Route("api/[controller]")]
    public class KanbanOrderController : Controller
    {
        private readonly DemoDbContext _context;

        public KanbanOrderController(DemoDbContext context)
        {
            _context = context;
        }

        [HttpPost("UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] JsonElement data)
        {
            if (!data.TryGetProperty("values", out var valuesElement))
                return BadRequest();

            var valuesStr = valuesElement.GetString();
            if (string.IsNullOrEmpty(valuesStr))
                return BadRequest("Empty values");

            string[] statuses;
            try
            {
                using var doc = JsonDocument.Parse(valuesStr);
                if (!doc.RootElement.TryGetProperty("Statuses", out var statusesElement) || statusesElement.ValueKind != JsonValueKind.Array)
                    return BadRequest("Statuses are missing");

                statuses = statusesElement.EnumerateArray().Select(e => e.GetString()).ToArray();
            }
            catch (Exception ex)
            {
                return BadRequest("Deserialization error: " + ex.Message);
            }

            if (statuses == null || statuses.Length == 0)
                return BadRequest("Statuses are missing");

            var taskLists = _context.TaskLists
                .Where(tl => statuses.Contains(tl.ListName))
                .ToList();

            for (int i = 0; i < statuses.Length; i++)
            {
                var status = statuses[i];
                var taskList = taskLists.FirstOrDefault(tl => tl.ListName == status);
                if (taskList != null)
                {
                    taskList.OrderIndex = i + 1;
                }
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("GetOrder")]
        public IActionResult GetOrder()
        {
            return Ok(_context.TaskLists.ToList());
        }
    }
}