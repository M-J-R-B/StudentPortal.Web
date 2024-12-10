using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StudentPortal.Web.Models.Entities
{
    public class Schedule
    {
        public int Id { get; set; }

        [Key]
        [Display(Name = "EDP Code")]
        public string EdpCode { get; set; }

        [ForeignKey("Subject")]

        [Required]
        [Display(Name = "Subject Code")]
        public string SubjCode { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }

        [Required]
        public string Room { get; set; }

        [Required]
        public string Section { get; set; }

        [Required]
        [Display(Name = "School Year")]
        public string SchoolYear { get; set; }
        public virtual Subject Subject { get; set; }


    }
}