using System;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data.Repositories;
using Assignment.Contracts.DTO;
using Assignment.Core.Data.Repositories;
using Assignment.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Infrastructure.Data.Repositories;

public class SlotDetailsRepository : Repository<Slot>, ISlotDetailsRepository
{
    private new readonly DatabaseContext _context;
    public SlotDetailsRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> CheckDateAsync(TimeSpan newStartTime, TimeSpan newEndTime, int userId, DateTime newDate)
    {

        var isSlotAvailable = _context.Slots
          .AsEnumerable()  // Forces execution in memory
          .Any(s => s.UserId == userId &&
                    s.Date == newDate && s.StartTime < newEndTime &&
                    s.EndTime > newStartTime);

        return isSlotAvailable;

    }

    public async Task<bool> createSlotAsync(Slot slot)
    {
        try
        {
            _context.Slots.Add(slot);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false; // Return false if an exception occurs while saving changes
        }
    }

    public async Task<List<Slot>> GetSlotByUserId(int UserId)
        {
            return await _context.Slots
                .Where(sd => sd.UserId == UserId)
                .ToListAsync();
        }

}
