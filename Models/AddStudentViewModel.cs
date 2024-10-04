namespace StudentPortal.Web.Models
{
    public class AddStudentViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

		public string Course { get; set; }
		public int Year { get; set; }
	}
}
