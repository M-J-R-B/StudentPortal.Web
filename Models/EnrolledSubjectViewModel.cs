namespace StudentPortal.Web.Models
{
    public class EnrolledSubjectViewModel
    {
        public string EdpCode { get; set; }
        public string SubjectCode { get; set; }
        public string Description { get; set; }
        public int Units { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Room { get; set; }
        public string Section { get; set; }
    }
}
