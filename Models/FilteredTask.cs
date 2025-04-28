using System.ComponentModel.DataAnnotations;

namespace DevExtremeVSTemplateMVC.Models
{
    public class FilteredTask {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Manager { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? Progress { get; set; }
        public string Company { get; set; }
        public string Text { get; set; }
        public string Owner { get; set; }
    }
}
