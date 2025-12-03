using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models
{
    public class Recipe
   {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Steps { get; set; } = string.Empty;
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        
        // Foreign key to User
        public int? OwnerUserId { get; set; }   // nullable so you can have public recipes

        // Optional navigation property
        public User? Owner { get; set; }
    }
}
