using DevExtremeVSTemplateMVC.DAL;
using DevExtremeVSTemplateMVC.Models;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult UpdateOrder([FromBody] KanbanOrderDto orderDto)
        {
            if (orderDto.Statuses == null)
                return BadRequest();

            // Fetch all TaskLists that match the provided statuses
            var taskLists = _context.TaskLists
                .Where(tl => orderDto.Statuses.Contains(tl.ListName))
                .ToList();

            // Update OrderIndex based on new order
            for (int i = 0; i < orderDto.Statuses.Length; i++)
            {
                var status = orderDto.Statuses[i];
                var taskList = taskLists.FirstOrDefault(tl => tl.ListName == status);
                if (taskList != null)
                {
                    taskList.OrderIndex = i+1;
                }
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("GetOrder")]
        public IActionResult GetOrder()
        {
            //var userId = "demo-user";
            //var order = _context.KanbanOrders.FirstOrDefault(x => x.UserId == userId);
            return Ok(_context.TaskLists.ToList());
        }
    }

    public class KanbanOrderDto
    {
        public string[] Statuses { get; set; }
    }
}