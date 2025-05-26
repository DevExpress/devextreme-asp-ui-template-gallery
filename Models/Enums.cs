using System.ComponentModel.DataAnnotations;

namespace DevExtremeVSTemplateMVC.Models
{
    public enum PlanningTaskStatus
    {
        Open,
        [Display(Name = "In Progress")]
        InProgress,
        Deferred,
        Completed
    }

    public enum PlanningTaskPriority
    {
        Low, Normal, High
    }
}
