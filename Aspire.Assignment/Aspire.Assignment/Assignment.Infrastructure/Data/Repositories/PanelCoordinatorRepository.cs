using System;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data.Repositories;
using Assignment.Core.Data.Repositories;
using Assignment.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Infrastructure.Data.Repositories;

public class PanelCoordinatorRepository : Repository<PanelCoordinator>, IPanelCoordinatorRepository
{
    private new readonly DatabaseContext _context;

    public PanelCoordinatorRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> AddAllocationAsync(AllocateDate allocation)
    {
        try
        {
            // Add the allocation to the database
            _context.AllocateDates.Add(allocation);
            await _context.SaveChangesAsync();
            return true; // Return true if the allocation is added successfully
        }
        catch
        {
            return false; // Return false if an exception occurs while saving changes
        }
    }

    public async Task<bool> CheckStartDateAsync(DateTime newStartDate, int UserId)
    {
        // Retrieve the latest end date for the given email
    DateTime? previousEndDate = await _context.AllocateDates
        .Where(entity => entity.PanelMemberID == UserId)
        .OrderByDescending(entity => entity.EndDate)
        .Select(entity => entity.EndDate)
        .FirstOrDefaultAsync();

    // Check if the new start date is after the latest end date or if there are no previous allocations
    bool isStartDateAfterPreviousEndDate = previousEndDate == null || newStartDate > previousEndDate;

    return isStartDateAfterPreviousEndDate;
    }
}
