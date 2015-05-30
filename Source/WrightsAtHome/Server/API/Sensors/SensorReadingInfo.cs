namespace WrightsAtHome.Server.API.Sensors
{
    public class SensorReadingInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SensorType { get; set; }
        public int Reading { get; set; }
        public string SmallImageUrl { get; set; }
        public string LargeImageUrl { get; set; }
    }
}
