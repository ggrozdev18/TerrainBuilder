using Microsoft.AspNetCore.Routing.Internal;
using System.ComponentModel.DataAnnotations;

namespace TerrainBuilder.Data
{
    public class Terrain
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public List<GeoPoint> GeoPoints { get; set; }
    }
}
