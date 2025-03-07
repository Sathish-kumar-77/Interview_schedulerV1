using System;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data.Repositories;
using Assignment.Core.Data.Repositories;
using Assignment.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Infrastructure.Data.Repositories   ;

public class AllocatedateRepository : Repository<AllocateDate>, IAllocatedateRepository
{
    public AllocatedateRepository(DatabaseContext context) : base(context)
    {
    }

         public async Task<IEnumerable<AllocateDate>> GetAllAsync()
        {
            return await _context.AllocateDates.ToListAsync();
        }
}
