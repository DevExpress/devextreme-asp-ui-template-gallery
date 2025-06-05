namespace DevExtremeVSTemplateMVC.Models
{
    public class TaskListPartialViewModel
    {
        public List<TaskModel> TaskList { get; set; }
        public int StatusIndex { get; set; }
        public string Status { get; set; }
    }
}
