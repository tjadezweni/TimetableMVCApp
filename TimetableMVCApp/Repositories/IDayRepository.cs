using System.Linq.Expressions;
using TimetableMVCApp.Models;

namespace TimetableMVCApp.Repositories;

public interface IDayRepository : IAsyncRepository<Day>
{
    public Task<List<Day>> GetDaysWithTimeAsync(Expression<Func<Day, bool>> expression);

    public Task<Day?> GetDayAsync(Expression<Func<Day, bool>> expression);
}
