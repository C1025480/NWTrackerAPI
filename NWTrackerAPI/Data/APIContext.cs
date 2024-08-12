using Microsoft.EntityFrameworkCore;
using NWTrackerAPI.Models;

namespace NWTrackerAPI.Data
{
    public class APIContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public APIContext(DbContextOptions<APIContext> options)
            : base(options)
        {

        }

    }
}