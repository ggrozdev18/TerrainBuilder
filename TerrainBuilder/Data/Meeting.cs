using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int MeetingTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(MeetingTypeId))]
        public MeetingType MeetingType { get; set; }

        [NotMapped]
        public List<SelectListItem> MeetingTypes { get; set; } = new List<SelectListItem>();

        public int MeetingStatusId { get; set; }
        
        [Required]
        [ForeignKey(nameof(MeetingStatusId))]
        public MeetingStatus MeetingStatus { get; set; }

        [NotMapped]
        public List<SelectListItem> MeetingStatuses { get; set; } = new List<SelectListItem>();

        public int CityId { get; set; }

        [Required]
        [ForeignKey(nameof(CityId))]
        public City City { get; set; }

        [NotMapped]
        public List<SelectListItem> Cities { get; set; } = new List<SelectListItem>();
    }
}
