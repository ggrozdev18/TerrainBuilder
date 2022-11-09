using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TerrainBuilder.Data;

namespace TerrainBuilder.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Terrain> Terrains { get; set; }

        public DbSet<TerrainBuilder.Data.Meeting> Meeting { get; set; }

        public DbSet<TerrainBuilder.Data.MeetingStatus> MeetingStatus { get; set; }

        public DbSet<TerrainBuilder.Data.MeetingType> MeetingType { get; set; }
    }
}