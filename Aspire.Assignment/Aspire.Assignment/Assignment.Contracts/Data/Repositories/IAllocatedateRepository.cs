using System;
using Assignment.Contracts.Data.Entities;

namespace Assignment.Contracts.Data.Repositories;

public interface IAllocatedateRepository : IRepository<AllocateDate>
{
    Task<IEnumerable<AllocateDate>> GetAllAsync();
}
