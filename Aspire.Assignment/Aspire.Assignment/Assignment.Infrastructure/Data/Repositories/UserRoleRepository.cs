using System;
using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data.Repositories;
using Assignment.Contracts.DTO;
using Assignment.Core.Data.Repositories;
using Assignment.Migrations;

namespace Assignment.Infrastructure.Data.Repositories;

public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
{   
    
     private readonly DatabaseContext _dbContext;
    public UserRoleRepository(DatabaseContext context) : base(context)
    {
         _dbContext = context;
    }

    public Task AddRoleAsync(Role role)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UsersDTO>> GetUserRoleAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserRole>> GetUserRolesAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
