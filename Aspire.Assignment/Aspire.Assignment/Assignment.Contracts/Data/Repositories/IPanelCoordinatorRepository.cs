using System;
using Assignment.Contracts.Data.Entities;

namespace Assignment.Contracts.Data.Repositories;

public interface IPanelCoordinatorRepository
{
    Task<bool> CheckStartDateAsync(DateTime newStartDate, int UserId);
    Task<bool> AddAllocationAsync(AllocateDate allocation);
}
