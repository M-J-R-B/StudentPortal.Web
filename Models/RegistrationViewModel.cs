using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class RegistrationViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is Required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is Required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
