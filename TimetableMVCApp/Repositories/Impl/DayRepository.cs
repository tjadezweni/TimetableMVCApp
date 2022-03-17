using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TimetableMVCApp.Models;
using TimetableMVCApp.Repositories;

namespace TimetableMVCApp.Repositories.Impl;

public class DayRepository : BaseRepository<Day>, IDayRepository
{
    public DayRepository(SchoolDBContext context)
        : base(context)
    { }

    public async Task<Day?> GetDayAsync(Expression<Func<Day, bool>> expression)
    {
        return await _dbSet.Include(day => day.Time).Where(expression).FirstOrDefaultAsync();
    }

    public async Task<List<Day>> GetDaysWithTimeAsync(Expression<Func<Day, bool>> expression)
    {
        return await _dbSet.Include(day => day.Time).Where(expression).ToListAsync();
    }
}
