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
}
}
