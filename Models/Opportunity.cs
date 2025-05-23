using System.ComponentModel.DataAnnotations;

namespace DevExtremeVSTemplateMVC.Models
{
    public class Opportunity
    {
        [Key]
        public int OpportunityId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
