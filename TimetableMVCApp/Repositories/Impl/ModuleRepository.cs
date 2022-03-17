using TimetableMVCApp.Models;
using TimetableMVCApp.Repositories;

namespace TimetableMVCApp.Repositories.Impl;

public class ModuleRepository : BaseRepository<Module>, IModuleRepository
{
    public ModuleRepository(SchoolDBContext context)
        : base(context)
        { }
}
