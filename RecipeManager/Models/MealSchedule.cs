using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models
{
    public class MealSchedule
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        public int? RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }
}