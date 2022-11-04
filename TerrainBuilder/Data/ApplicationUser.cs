using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TerrainBuilder.Data
{
    public class ApplicationUser : IdentityUser
    {
        

        public List<Terrain> Terrains { get; set; } = new List<Terrain>();
    }
}
