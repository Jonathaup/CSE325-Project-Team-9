using RecipeManager.Models;


namespace RecipeManager.Services
{
    public interface IShoppingListService
    {
        Task<ShoppingList?> GetByUserIdAsync(string userId);
        Task AddOrUpdateAsync(ShoppingList list);
        Task ClearAsync(string userId);
    }
}