using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models
{
    // Model class representing a recipe with its details and ingredients
    public class Recipe
   {
        // Primary key for the recipe
        public int Id { get; set; }
        
        // Title/name of the recipe (initialized to empty string)
        public string Title { get; set; } = string.Empty;
        
        // Cooking instructions/steps for the recipe (initialized to empty string)
        public string Steps { get; set; } = string.Empty;
        
        // Collection of ingredients associated with this recipe (initialized to empty list)
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}