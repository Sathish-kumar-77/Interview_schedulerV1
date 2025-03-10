using System;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data.Repositories;
using Assignment.Core.Data.Repositories;
using Assignment.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Infrastructure.Data.Repositories   ;

public class AllocatedateRepository : Repository<AllocateDate>, IAllocatedateRepository
{
     private new readonly DatabaseContext _context;
    public AllocatedateRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<AllocateDate>> GetPanelAllocationByUserId(int UserId)
        {
            return await _context.AllocateDates
                .Where(sd => sd.PanelMemberID == UserId)
                .ToListAsync();
        }

}
