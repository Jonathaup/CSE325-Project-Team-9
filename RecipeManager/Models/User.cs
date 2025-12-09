using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(100)]
        public string DisplayName { get; set; } = string.Empty;
    }
}
