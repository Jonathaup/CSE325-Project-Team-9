using Microsoft.EntityFrameworkCore;
using RecipeManager.Data;
using RecipeManager.Models;

namespace RecipeManager.Services
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly ApplicationDbContext _context;

        public ShoppingListService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ShoppingList?> GetByUserIdAsync(string userId)
        {
            return await _context.ShoppingLists
                .Include(l => l.Items)
                .FirstOrDefaultAsync(l => l.UserId == userId);
        }

        public async Task AddOrUpdateAsync(ShoppingList list)
        {
            var existing = await GetByUserIdAsync(list.UserId);

            if (existing == null)
                _context.ShoppingLists.Add(list);
            else
                _context.ShoppingLists.Update(list);

            await _context.SaveChangesAsync();
        }

        public async Task ClearAsync(string userId)
        {
            var list = await GetByUserIdAsync(userId);

            if (list != null)
            {
                _context.ShoppingLists.Remove(list);
                await _context.SaveChangesAsync();
            }
        }
    }
}
