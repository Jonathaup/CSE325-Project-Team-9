using Microsoft.EntityFrameworkCore;
using RecipeManager.Data;
using RecipeManager.Models;

namespace RecipeManager.Services
{
    // Service class that handles all recipe-related database operations
    public class RecipeService
    {
        // Database context for accessing recipe data
        private readonly ApplicationDbContext _context;

        // Constructor: injects the database context
        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieves all recipes from the database including their ingredients
        public async Task<List<Recipe>> GetAllAsync() =>
            await _context.Recipes.Include(r => r.Ingredients).ToListAsync();

        // Retrieves a single recipe by ID including its ingredients
        // Uses AsNoTracking to prevent tracking issues when reading data
        public async Task<Recipe?> GetByIdAsync(int id) =>
            await _context.Recipes
                .Include(r => r.Ingredients)
                .AsNoTracking()  // Important to avoid tracking issues
                .FirstOrDefaultAsync(r => r.Id == id);

        // Adds a new recipe to the database
        public async Task AddAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
        }

        // Updates an existing recipe and its ingredients
        public async Task UpdateAsync(Recipe recipe)
        {
            // Retrieve the existing recipe with its ingredients from the database
            var existingRecipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == recipe.Id);

            // Exit if recipe doesn't exist
            if (existingRecipe == null)
                return;

            // Update basic recipe properties
            existingRecipe.Title = recipe.Title;
            existingRecipe.Steps = recipe.Steps;

            // Get current and incoming ingredient lists
            var existingIngredients = existingRecipe.Ingredients.ToList();
            var incomingIngredients = recipe.Ingredients.ToList();

            // Remove ingredients that are no longer in the updated recipe
            foreach (var existing in existingIngredients)
            {
                if (!incomingIngredients.Any(i => i.Id == existing.Id))
                {
                    _context.Ingredients.Remove(existing);
                }
            }

            // Update existing ingredients or add new ones
            foreach (var incoming in incomingIngredients)
            {
                var existing = existingIngredients.FirstOrDefault(i => i.Id == incoming.Id);

                if (existing != null)
                {
                    // Update existing ingredient properties
                    existing.Name = incoming.Name;
                    existing.Quantity = incoming.Quantity;
                    existing.Unit = incoming.Unit;
                }
                else
                {
                    // Add new ingredient to the recipe
                    incoming.RecipeId = recipe.Id;
                    existingRecipe.Ingredients.Add(incoming);
                }
            }

            // Save all changes to the database
            await _context.SaveChangesAsync();
        }

        // Deletes a recipe and its associated ingredients from the database
        public async Task DeleteAsync(int id)
        {
            // Retrieve the recipe with its ingredients
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);

            // Remove the recipe if it exists (ingredients will be cascade deleted)
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }
    }
}