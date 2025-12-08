using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public List<ShoppingListItem> Items { get; set; } = new();
    }
}
