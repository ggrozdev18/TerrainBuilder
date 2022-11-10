﻿using Microsoft.AspNetCore.Routing.Internal;
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

        [Required]
        [Range(0, 100, ErrorMessage = "Must be between 1 and 100")]
        public int Length { get; set; }


        [Required]
        [Range(0, 100, ErrorMessage = "Must be between 1 and 100")]
        public int Width { get; set; }

        [Required]
        [Range(0, 1000000000, ErrorMessage = "Must be between 1 and 1 000 000 000")]
        public double OffsetX { get; set; }

        [Required]
        [Range(0, 1000000000, ErrorMessage = "Must be between 1 and 1 000 000 000")]
        public double OffsetY { get; set; }

        [Required]
        [Range(0, 6, ErrorMessage = "Must be between 1 and 6")]
        public int Octaves { get; set; }

        [Required]
        [Range(0.5, 1)]
        public double Zoom { get; set; }

        [Required]
        [Range(0.5, 1)]
        public double Power { get; set; }

        [Required]
        [Range(0, 4, ErrorMessage = "Must be between 1 and 4")]
        public double Influence { get; set; }
    }
}
