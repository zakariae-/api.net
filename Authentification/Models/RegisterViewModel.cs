using System.ComponentModel.DataAnnotations;

namespace Authentification.Models
{
    public class RegisterViewModel
    {
        [Required]
        //[StrictEmailAddress]
        public string Email { get; set; }
    }
}
