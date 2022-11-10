using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TerrainBuilder.Data
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string FirstName { get; set; }

        public List<Terrain> Terrains { get; set; } = new List<Terrain>();
    }
}
