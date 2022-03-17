using TimetableMVCApp.Models;
using TimetableMVCApp.Repositories;

namespace TimetableMVCApp.Repositories.Impl;

public class TimeRepository : BaseRepository<Time>, ITimeRepository
{
    public TimeRepository(SchoolDBContext context)
        : base(context)
    { }
}
