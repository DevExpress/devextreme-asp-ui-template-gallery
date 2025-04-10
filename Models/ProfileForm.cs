using System.ComponentModel.DataAnnotations;

namespace DevExtremeVSTemplateMVC.Models
{
    public class EmployeeProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; } 
        public string Status { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Manager { get; set; }
        public string Phone { get; set; }

        [RegularExpression(@"^[\d\w._-]+@[\d\w._-]+\.[\w]+$", ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        public string Address { get; set; }
        public List<Activity> Activities { get; set; }
        public List<Opportunity> Opportunities { get; set; }
        public List<TaskItem> Tasks { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HiredDate { get; set; }
        public string Department { get; set; }
        public string DomainUsername { get; set; }
        public string Country { get; set; }
        public string Supervisor { get; set; }
    }

    public class Activity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Manager { get; set; }
    }

    public class Opportunity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class TaskItem
    {
        public string Text { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }
        public string? Priority { get; set; }
        public string Manager { get; set; }
    }
}
