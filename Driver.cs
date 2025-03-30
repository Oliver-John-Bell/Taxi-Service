using SQLite;

namespace GRoupHI
{
    public class Driver
    {
        [PrimaryKey, AutoIncrement]
        public int DId { get; set; }
        public string Name { get; set; }
        public string CarModel { get; set; }
        public double Rating { get; set; }
        public string? ImageSource { get; set; }
        public int EstimatedTimeDriver { get; set; }
        public int EstimatedTimeTrip { get; set; }
        public string StartPiont { get; set; }
        public int DriverPhoneNumber { get; set; }
    }
}
