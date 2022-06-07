using System.ComponentModel.DataAnnotations;

namespace BonoApp.API.User.Resources
{
    public class SaveUserResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }
        
        [Required]
        [MaxLength(60)]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
    }
}