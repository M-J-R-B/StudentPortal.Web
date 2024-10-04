using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models.Entities
{
    [Index(nameof(Username), IsUnique = true)]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is Required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is Required.")]
        public string Password { get; set; }

    }
}
