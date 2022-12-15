using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerrainBuilder.Data
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string? FirstName { get; set; }

        [NotMapped]
        public bool isUser { get; set; } = false;

        [NotMapped]
        public bool isAdmin { get; set; } = false;

public List<Terrain> Terrains { get; set; } = new List<Terrain>();
    }
}
