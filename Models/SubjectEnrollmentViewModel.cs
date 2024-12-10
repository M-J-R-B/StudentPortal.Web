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
    }
}
