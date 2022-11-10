﻿using System.ComponentModel.DataAnnotations;

namespace TerrainBuilder.Data
{
    public class MeetingStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
