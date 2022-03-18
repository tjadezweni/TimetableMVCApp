using System.Linq.Expressions;
using TimetableMVCApp.Models;

namespace TimetableMVCApp.Repositories;

public interface IModuleRepository : IAsyncRepository<Module>
{
    public Task<List<Module>> GetModulesWithDaysAsync(Expression<Func<Module, bool>> expression);

}
