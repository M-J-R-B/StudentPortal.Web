using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models.Entities
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        public string Subj_Code { get; set; } = string.Empty;

        public string SubjectDescription { get; set; } = string.Empty;

        public int Units { get; set; }

        public string Offering { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        // Fixed property name by replacing '/' with '_'
        public string Course_Code { get; set; } = string.Empty;

        public int CurriculumYear { get; set; }

        public string? SubjectRequisite { get; set; }

        public bool? IsPreRequisite { get; set; }
    }
}
