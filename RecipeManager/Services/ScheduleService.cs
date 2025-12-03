using Microsoft.EntityFrameworkCore;
using RecipeManager.Data;
using RecipeManager.Models;


namespace RecipeManager.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDbContext _context;


        public ScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<MealSchedule>> GetAllAsync() =>
        await _context.MealSchedules.ToListAsync();


        public async Task<MealSchedule?> GetByIdAsync(int id) =>
        await _context.MealSchedules.FirstOrDefaultAsync(s => s.Id == id);


        public async Task AddAsync(MealSchedule schedule)
        {
            _context.MealSchedules.Add(schedule);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(MealSchedule schedule)
        {
            _context.MealSchedules.Update(schedule);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var schedule = await _context.MealSchedules.FindAsync(id);
            if (schedule != null)
            {
                _context.MealSchedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
        }
    }
}