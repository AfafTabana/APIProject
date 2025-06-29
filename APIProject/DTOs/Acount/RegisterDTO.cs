using System.ComponentModel.DataAnnotations;

namespace APIProject.DTOs.Acount
{
    public class RegisterDTO
    {
        [Required]
        public string  UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string  Password { get; set; }
        [Required]
        [Compare ("Password" , ErrorMessage = "Passwaed is not match ")]
        public string  ConfirmPassward { get; set; }
     
    }
}
