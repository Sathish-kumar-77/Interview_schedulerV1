using Assignment.Contracts.Data.Repositories;

namespace Assignment.Contracts.Data
{
    public interface IUnitOfWork
    {
        IAppRepository App { get; }
        IUserRepository User { get; }

        IUsersRepository Users {get; }
        IAllocatedateRepository AllocateDate {get;}
<<<<<<< HEAD

        ISlotDetailsRepository Slot {get;}
        
=======
<<<<<<< HEAD
        IRoleRepository Roles{get;}
=======
        

>>>>>>> c1fd1ba70b7d9c2d7318c63b179ab640b489ac66
>>>>>>> 18db4e69d96ac9ea416a3f16eb0bb158ff3b886b
        IPanelCoordinatorRepository PanelCoordinator {get;}
        Task CommitAsync();
    }
}