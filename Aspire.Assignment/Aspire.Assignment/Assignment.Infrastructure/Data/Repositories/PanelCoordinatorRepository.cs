using System;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data.Repositories;
using Assignment.Core.Data.Repositories;
using Assignment.Migrations;

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

}
