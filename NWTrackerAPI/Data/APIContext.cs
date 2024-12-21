using Microsoft.EntityFrameworkCore;
using NWTrackerAPI.Models;

namespace NWTrackerAPI.Data
{
    public class APIContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<TT_TRACKER> TT_TRACKER { get; set; }
        public DbSet<SS_SUPPORT_STATUS> SS_SUPPORT_STATUS { get; set; }

        public APIContext(DbContextOptions<APIContext> options)
            : base(options)
        {

        }

    }
}