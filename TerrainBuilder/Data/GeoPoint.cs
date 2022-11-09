namespace TerrainBuilder.Data
{
    public class GeoPoint
    {
        public Guid Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public double Elevation { get; set; }

        public double Humidity { get; set; }

        public double Temperature { get; set; }

        public double Hospitality { get; set; }
    }
}
