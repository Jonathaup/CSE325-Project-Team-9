using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

public double? Quantity { get; set; } = 0;

        public string? Unit { get; set; }

        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
