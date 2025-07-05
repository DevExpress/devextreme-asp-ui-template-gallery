namespace DevExtremeVSTemplateMVC.Models
{
    public class TaskListPartialViewModel
    {
        public List<EmployeeTask> TaskList { get; set; }
        public int StatusIndex { get; set; }
        public TaskList Status { get; set; }
    }
}
