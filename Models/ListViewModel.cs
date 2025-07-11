namespace DevExtremeVSTemplateMVC.Models
{
    public class ListViewModel
    {
        public List<EmployeeTask> TasksInList { get; set; }
        public int ListIndex { get; set; }
        public TaskList CurrentList { get; set; }
    }
}
