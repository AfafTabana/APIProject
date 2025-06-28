using System.ComponentModel.DataAnnotations.Schema;
namespace APIProject.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

      
    }
}
