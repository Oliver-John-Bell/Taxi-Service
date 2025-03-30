using SQLite;

namespace GRoupHI
{
    [Table("DBTrip")]
    public class DBTrip
    {
        [PrimaryKey, AutoIncrement]
        public int TId { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public decimal TripCost { get; set; }

        public string Rating { get; set; }
        
    }
}