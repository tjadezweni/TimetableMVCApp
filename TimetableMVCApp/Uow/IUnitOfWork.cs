using TimetableMVCApp.Repositories;

namespace TimetableMVCApp.Uow;

public interface IUnitOfWork
{
    public ITimeRepository _timeRepository { get; }
    public IDayRepository _dayRepository { get; }
    public IModuleRepository _moduleRepository { get; }
    Task CommitAsync();
}
