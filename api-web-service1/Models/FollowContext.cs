using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_web_service1.Models
{
    public class FollowContext : DbContext
    {
        public FollowContext(DbContextOptions<FollowContext> options)
            : base(options)
        {
        }

        public DbSet<Follow> Follows { get; set; }
    }
}
