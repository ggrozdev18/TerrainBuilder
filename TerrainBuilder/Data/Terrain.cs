using Microsoft.AspNetCore.Routing.Internal;
using System.ComponentModel.DataAnnotations;

namespace TerrainBuilder.Data
{
    public class Terrain
    {
        //test
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0, 150, ErrorMessage = "Must be between 1 and 150")]
        public int Length { get; set; }

        [Required]
        [Range(0, 150, ErrorMessage = "Must be between 1 and 150")]
        public int Width { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "Must be between 1 and 1 000 000")]
        public double OffsetX { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "Must be between 1 and 1 000 000")]
        public double OffsetY { get; set; }

        [Required]
        [Range(0, 7, ErrorMessage = "Must be between 1 and 6")]
        public int Octaves { get; set; }

        [Required]
        [Range(0.5, 1)]
        public double Zoom { get; set; } = 1;

        [Required]
        [Range(0.5, 1)]
        public double Power { get; set; } = 1;

        [Required]
        [Range(0, 4, ErrorMessage = "Must be between 1 and 4")]
        public double Influence { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
