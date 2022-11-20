using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TerrainBuilder.Data;
using TerrainBuilder.Models;

namespace TerrainBuilder.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Terrain> Terrains { get; set; }

        public DbSet<Meeting> Meetings { get; set; }

        public DbSet<MeetingStatus> MeetingStatuses { get; set; }

        public DbSet<MeetingType> MeetingTypes { get; set; }
        
        public DbSet<City> Cities { get; set; }
    }
}