using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Models
{
    public class SubjectEnrollmentViewModel
    {
        public int StudentId { get; set; }
        public string EdpCode { get; set; }
        public string SubjCode { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Room { get; set; }
        public string Section { get; set; }

        public string EncodedBy { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public List<EnrolledSubjectViewModel> EnrolledSubjects { get; set; }
        public int TotalUnits { get; set; }

        public string StudentName { get; set; }
        public string Course { get; set; }
        public int Year { get; set; }


        // Navigation properties
        public virtual Student Student { get; set; }
        public virtual Schedule Schedule { get; set; }


    }
}
