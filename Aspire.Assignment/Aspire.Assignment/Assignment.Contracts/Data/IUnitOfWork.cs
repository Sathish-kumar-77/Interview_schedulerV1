using Assignment.Contracts.Data.Repositories;

namespace Assignment.Contracts.Data
{
    public interface IUnitOfWork
    {
        IAppRepository App { get; }
        IUserRepository User { get; }

        IUsersRepository Users {get; }
        IAllocatedateRepository AllocateDate {get;}

        IRoleRepository Roles{get;}

        IPanelCoordinatorRepository PanelCoordinator {get;}
        Task CommitAsync();
    }
}