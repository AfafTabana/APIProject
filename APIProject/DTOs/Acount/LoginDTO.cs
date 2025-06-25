using System.ComponentModel.DataAnnotations;

namespace APIProject.DTOs.Acount
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
         
        public string Password { get; set; }
         
    }
}
