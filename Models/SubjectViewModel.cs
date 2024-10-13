using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models
{
    public class SubjectViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Subject Code is required.")]
        [Display(Name = "Subject Code")]
        public string Subj_Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Subject Description is required.")]
        [Display(Name = "Subject Description")]
        public string SubjectDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Units are required.")]
        [Range(1, 10, ErrorMessage = "Units must be between 1 and 10.")]
        public int Units { get; set; }

        [Required(ErrorMessage = "Offering is required.")]
        public string Offering { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course Code is required.")]
        [Display(Name = "Course Code")]
        public string Course_Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Curriculum Year is required.")]
        [Display(Name = "Curriculum Year")]
        public int CurriculumYear { get; set; }

        [Display(Name = "Subject Requisite")]
        public string? SubjectRequisite { get; set; }

        [Display(Name = "Is Pre-Requisite")]
        public bool? IsPreRequisite { get; set; }
    }
}
