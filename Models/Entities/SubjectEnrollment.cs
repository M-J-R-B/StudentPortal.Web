using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models.Entities
{
    public class SubjectEnrollment
    {
        
        
            [Key]
            public int Id { get; set; }
            public int StudentId { get; set; }
            public string EdpCode { get; set; }
            public string EncodedBy { get; set; }
            public DateTime EncodedDate { get; set; }

            // Navigation properties matching project pattern
            public virtual Student Student { get; set; }
            public virtual Schedule Schedule { get; set; }
        
    }
}
