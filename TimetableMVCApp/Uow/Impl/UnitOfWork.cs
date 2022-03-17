using TimetableMVCApp.Models;
using TimetableMVCApp.Repositories;

namespace TimetableMVCApp.Uow.Impl;

public class UnitOfWork : IUnitOfWork
{
    private readonly SchoolDBContext _context;

    #region Repositories
    public ITimeRepository _timeRepository { get; }
    public IDayRepository _dayRepository { get; }
    public IModuleRepository _moduleRepository { get; }
    #endregion

    public UnitOfWork(
        SchoolDBContext context,
        ITimeRepository timeRepository,
        IDayRepository dayRepository,
        IModuleRepository moduleRepository)
    {
        _context = context;
        _timeRepository = timeRepository;
        _dayRepository = dayRepository;
        _moduleRepository = moduleRepository;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
