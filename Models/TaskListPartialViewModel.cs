namespace DevExtremeVSTemplateMVC.Models
{
    public class TaskListPartialViewModel
    {
        public List<EmployeeTask> TasksInList { get; set; }
        public int ListIndex { get; set; }
        public TaskList CurrentList { get; set; }
    }
}
