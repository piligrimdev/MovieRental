using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data
{
    public class CustomAppUser : IdentityUser
    {
        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }
    }
}
