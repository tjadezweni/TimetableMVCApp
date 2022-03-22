using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TimetableMVCApp.Models;
using TimetableMVCApp.Repositories;

namespace TimetableMVCApp.Repositories.Impl;

public class ModuleRepository : BaseRepository<Module>, IModuleRepository
{
    public ModuleRepository(SchoolDBContext context)
        : base(context)
        { }

    public async Task<List<Module>> GetModulesWithDaysAsync(Expression<Func<Module, bool>> expression)
    {   var modules = await _dbSet.Include(module => module.Days).ThenInclude(day => day.Time).Where(expression).ToListAsync();
        return modules;
    }
}
