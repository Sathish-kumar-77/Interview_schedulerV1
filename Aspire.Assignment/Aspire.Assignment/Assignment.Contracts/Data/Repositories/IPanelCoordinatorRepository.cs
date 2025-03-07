using System;
using Assignment.Contracts.Data.Entities;

namespace Assignment.Contracts.Data.Repositories;

public interface IPanelCoordinatorRepository
{
    Task<bool> AddAllocationAsync(AllocateDate allocation);
}
