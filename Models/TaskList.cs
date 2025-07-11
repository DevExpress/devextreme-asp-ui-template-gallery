using System.ComponentModel.DataAnnotations;

namespace DevExtremeVSTemplateMVC.Models
{
    public class TaskList
    {
        [Key]
        public int Id { get; set; }
        public string ListName { get; set; }
        public int OrderIndex { get; set; }
        public bool Visible { get; set; } = true;
    }
}
