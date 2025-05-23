using System.ComponentModel.DataAnnotations;

namespace DevExtremeVSTemplateMVC.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Manager { get; set; }
    }
}
