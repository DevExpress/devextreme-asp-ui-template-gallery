using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace DevExtremeVSTemplateMVC.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public ContactState State { get; set; }
        public int ZipCode { get; set; }
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
        public List<EmployeeTask> Tasks { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HiredDate { get; set; }
        public string Department { get; set; }
        public string DomainUsername { get; set; }
        public string Country { get; set; }
        public string Supervisor { get; set; }
    }
}
