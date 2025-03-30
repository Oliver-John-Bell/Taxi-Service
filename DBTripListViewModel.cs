using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GRoupHI
{
    public class DBTripListViewModel : INotifyPropertyChanged
    {
        // Create a connection to the trip database
        TripDatabase tripDatabase = new TripDatabase();

        // Constructor for the view model
        public DBTripListViewModel()
        {
            // Initialize the trip database
            AllTrips = tripDatabase.GetTrips();
            //System.Diagnostics.Debug.WriteLine($"test: {AllTrips[7].TId}");


        }

        // The Trips property that holds the details of all trips
        private ObservableCollection<DBTrip> alltrips;
        public ObservableCollection<DBTrip> AllTrips
        {
            get { return alltrips; }
            set
            {
                if (value != null)
                {
                    alltrips = value;
                    OnPropertyChanged("AllTrips");
                }
            }
        }

        private string searchTerm;
        public string SearchTerm
        {
            get
            {
                return searchTerm;
            }
            set
            {
                if (value != null)
                {
                    searchTerm = value;
                    OnPropertyChanged("SearchTerm");

                    AllTrips = tripDatabase.SearchTrips(searchTerm);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
