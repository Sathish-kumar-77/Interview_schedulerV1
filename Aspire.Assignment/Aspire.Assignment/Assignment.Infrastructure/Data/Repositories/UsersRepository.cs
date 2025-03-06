using System;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data.Repositories;
using Assignment.Core.Data.Repositories;
using Assignment.Migrations;

namespace Assignment.Infrastructure.Data.Repositories;

public class UsersRepository : Repository<Users>, IUsersRepository
{
    public UsersRepository(DatabaseContext context) : base(context)
    {
    }
}
