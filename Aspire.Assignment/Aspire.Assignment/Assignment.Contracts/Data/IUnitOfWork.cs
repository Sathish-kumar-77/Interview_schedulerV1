using Assignment.Contracts.Data.Repositories;

namespace Assignment.Contracts.Data
{
    public interface IUnitOfWork
    {
        IAppRepository App { get; }
        IUserRepository User { get; }

        IUsersRepository Users {get; }
        IAllocatedateRepository AllocateDate {get;}

        IPanelCoordinatorRepository PanelCoordinator {get;}

        ISlotDetailsRepository Slot {get;}
        
        IRoleRepository Roles{get;}


        Task CommitAsync();
    }
}