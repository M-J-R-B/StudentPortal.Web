using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Course { get; set; }
        public int Year { get; set; }
    }
}
