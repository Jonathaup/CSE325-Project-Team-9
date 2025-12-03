using RecipeManager.Models;


namespace RecipeManager.Services
{
    public interface IScheduleService
    {
        Task<List<MealSchedule>> GetAllAsync();
        Task<MealSchedule?> GetByIdAsync(int id);
        Task AddAsync(MealSchedule schedule);
        Task UpdateAsync(MealSchedule schedule);
        Task DeleteAsync(int id);
    }
}