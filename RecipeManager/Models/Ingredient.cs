using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models
{
    // Model class representing an ingredient in a recipe
    public class Ingredient
    {
        // Primary key for the ingredient
        public int Id { get; set; }

        // Name of the ingredient (required field)
        [Required]
        public string? Name { get; set; }

        // Quantity of the ingredient (nullable, defaults to 0)
        public double? Quantity { get; set; } = 0;

        // Unit of measurement for the ingredient (e.g., "cups", "grams", "tbsp")
        public string? Unit { get; set; }

        // Foreign key linking to the parent recipe
        public int RecipeId { get; set; }
        
        // Navigation property to the parent recipe
        public Recipe? Recipe { get; set; }
    }
}