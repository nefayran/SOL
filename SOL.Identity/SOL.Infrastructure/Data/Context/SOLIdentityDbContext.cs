using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SOL.Identity.SOL.Application.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOL.Identity.SOL.Infrastructure.Data.Context
{
    public class SOLIdentityDbContext : IdentityDbContext<User>
    {
        public SOLIdentityDbContext(DbContextOptions<SOLIdentityDbContext> options)
                : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
