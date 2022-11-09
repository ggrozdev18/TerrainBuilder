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

        public int Length { get; set; }

        public int Width { get; set; }

        //public double[][] Heights { get; set; }

        public double OffsetX { get; set; }

        public double OffsetY { get; set; }

        public int Octaves { get; set; }

        public double Zoom { get; set; }

        public double Power { get; set; }

        public double Influence { get; set; }

        //public List<GeoPoint> GeoPoints { get; set; }
    }
}
