namespace DevExtremeVSTemplateMVC.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Owner { get; set; }
        public string Company { get; set; }
        public string Manager { get; set; }
        public int Progress { get; set; }
        public int ParentId { get; set; }
    }

}
