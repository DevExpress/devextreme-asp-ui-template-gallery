using System.ComponentModel.DataAnnotations;

namespace DevExtremeVSTemplateMVC.Models
{
    public class ContactState
    {
        [Key]
        public int SateId { get; set; }
        public string StateShort { get; set; }
        public string StateLong{ get; set; }
        public string StateCoords{ get; set; }
        public string Flag48px { get; set; }
        public string Flag24px { get; set; }
        public string SsmaTimeStamp { get; set; }
        public IList<Contact> Contacts { get; set; }
    }
}
