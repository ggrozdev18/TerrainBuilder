using System.ComponentModel.DataAnnotations;

namespace TerrainBuilder.Data
{
    public class Meeting
    {
        [Key]
        public int Id { get; set; }

        public DateTime MeetingDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Topic { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public MeetingType MeetingType { get; set; }

        [Required]
        public MeetingStatus MeetingStatus { get; set; }

        [Required]
        public City City { get; set; }
    }
}
