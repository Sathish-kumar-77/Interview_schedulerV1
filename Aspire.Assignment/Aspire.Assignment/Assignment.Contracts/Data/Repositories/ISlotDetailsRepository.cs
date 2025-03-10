using System;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;

namespace Assignment.Contracts.Data.Repositories;

public interface ISlotDetailsRepository : IRepository<Slot>
{
    Task<bool> CheckDateAsync(TimeSpan newStartTime,TimeSpan newEndTime, int UserId,DateTime Date);
    Task<bool> createSlotAsync(Slot slot);
    Task<List<Slot>> GetSlotByUserId(int UserId);

}
