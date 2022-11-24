using System.ComponentModel.DataAnnotations;

namespace TerrainBuilder.Models
{
    public class TerrainViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0, 150, ErrorMessage = "Must be between 1 and 150")]
        public int Length { get; set; }

        [Required]
        [Range(0, 150, ErrorMessage = "Must be between 1 and 150")]
        public int Width { get; set; }

        [Required]
       // [Range(0, 1000000, ErrorMessage = "Must be between 1 and 1 000 000")]
        public double OffsetX { get; set; }

        [Required]
        //[Range(0, 1000000, ErrorMessage = "Must be between 1 and 1 000 000")]
        public double OffsetY { get; set; }

        [Required]
        [Range(0, 7, ErrorMessage = "Must be between 1 and 6")]
        public int Octaves { get; set; }

        [Required]
        [Range(1, 4, ErrorMessage = "Must be between 1 and 4")]
        public double Influence { get; set; }

        public bool IsDBSaveSuccessful { get; set; } = false;
    }
}
