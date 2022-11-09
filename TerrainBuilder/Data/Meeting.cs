namespace TerrainBuilder.Data
{
    public class Meeting
    {
        public int Id { get; set; }

        public DateTime MeetingDate { get; set; }

        public string Topic { get; set; }

        public string Description { get; set; }

        public MeetingType MeetingType { get; set; }

        public MeetingStatus MeetingStatus { get; set; }

        public City City { get; set; }
    }
}
