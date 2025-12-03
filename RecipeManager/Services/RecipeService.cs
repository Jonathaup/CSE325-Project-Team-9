using Microsoft.EntityFrameworkCore;
using RecipeManager.Data;
using RecipeManager.Models;

namespace RecipeManager.Services
{
    public class RecipeService
    {
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Recipe>> GetAllAsync() =>
            await _context.Recipes.Include(r => r.Ingredients).ToListAsync();

        public async Task<Recipe?> GetByIdAsync(int id) =>
            await _context.Recipes
                .Include(r => r.Ingredients)
                .AsNoTracking()  // Importante para evitar problemas de seguimiento
                .FirstOrDefaultAsync(r => r.Id == id);

        public async Task AddAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Recipe recipe)
        {
            // Actualizar los datos básicos de la receta
            var existingRecipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == recipe.Id);

            if (existingRecipe == null)
                return;

            // Actualizar propiedades de la receta
            existingRecipe.Title = recipe.Title;
            existingRecipe.Steps = recipe.Steps;

            // Obtener los ingredientes existentes
            var existingIngredients = existingRecipe.Ingredients.ToList();
            var incomingIngredients = recipe.Ingredients.ToList();

            // Eliminar ingredientes que ya no están
            foreach (var existing in existingIngredients)
            {
                if (!incomingIngredients.Any(i => i.Id == existing.Id))
                {
                    _context.Ingredients.Remove(existing);
                }
            }

            // Actualizar o agregar ingredientes
            foreach (var incoming in incomingIngredients)
            {
                var existing = existingIngredients.FirstOrDefault(i => i.Id == incoming.Id);
                
                if (existing != null)
                {
                    // Actualizar ingrediente existente
                    existing.Name = incoming.Name;
                    existing.Quantity = incoming.Quantity;
                    existing.Unit = incoming.Unit;
                }
                else
                {
                    // Agregar nuevo ingrediente
                    incoming.RecipeId = recipe.Id;
                    existingRecipe.Ingredients.Add(incoming);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);
                
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }
    }
}