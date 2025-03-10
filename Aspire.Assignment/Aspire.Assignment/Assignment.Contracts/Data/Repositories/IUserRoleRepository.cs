using System;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;

namespace Assignment.Contracts.Data.Repositories;

public interface IUserRoleRepository : IRepository<UserRole>
{
    Task<IEnumerable<UserRole>> GetUserRolesAsync(Guid userId);
        Task<IEnumerable<UsersDTO>> GetUserRoleAsync();
        Task AddRoleAsync(Role role);
        Task SaveChangesAsync();
}
