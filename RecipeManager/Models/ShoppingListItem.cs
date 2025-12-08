using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models
{
    public class ShoppingListItem
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public double Quantity { get; set; }

        public string Unit { get; set; } = string.Empty;

        // Relationship
        public int ShoppingListId { get; set; }
        public ShoppingList? ShoppingList { get; set; }
    }
}
