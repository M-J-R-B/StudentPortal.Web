using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Username is Required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is Required.")]

        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
