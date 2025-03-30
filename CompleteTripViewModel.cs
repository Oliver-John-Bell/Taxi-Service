using System.ComponentModel;

namespace GRoupHI
{
    public class CompleteTripViewModel : INotifyPropertyChanged
    {
        
        // Create an instance of your database class
        TripDatabase tripDatabase = new TripDatabase();

        // Constructor
        public CompleteTripViewModel(DBTrip selectTrip)
        {
            // Load the selected trip
            SelectedTrip = selectTrip;
        }


        // Method to update the trip
        public void UpdateTrip()
        {
            // Create a new DBTrip object with updated values
            DBTrip updatedTrip = new DBTrip();
            updatedTrip.TId = SelectedTrip.TId;
            updatedTrip.Origin = Origin;
            updatedTrip.Destination = Destination;
            updatedTrip.TripCost = TripCost;
            updatedTrip.Rating = Rating;

            // Call the update method from the database
            tripDatabase.UpdateTrip(updatedTrip);
        }

        // Method to delete the trip
        public void DeleteTrip()
        {
            // Check if there's a selected trip
            if (SelectedTrip != null) // Use SelectedTrip
            {
                System.Diagnostics.Debug.WriteLine($"First: {tripDatabase.GetTrips().Count} {SelectedTrip.TId}");
                // Call the delete method from the database with the selected trip
                tripDatabase.DeleteTrip(SelectedTrip.TId); // Use SelectedTrip.TId
                System.Diagnostics.Debug.WriteLine($"Second: {tripDatabase.GetTrips().Count}");
            }
        }


        // Properties
        private DBTrip selectedTrip;
        public DBTrip SelectedTrip
        {
            get { return selectedTrip; }
            set
            {
                if (value != null)
                {
                    selectedTrip = value;
                    Origin = selectedTrip.Origin;
                    Destination = selectedTrip.Destination;
                    TripCost = selectedTrip.TripCost;
                    Rating = selectedTrip.Rating;
                    OnPropertyChanged("SelectedTrip");
                }
            }
        }

        

        private string origin;
        public string Origin
        {
            get { return origin; }
            set
            {
                origin = value;
                OnPropertyChanged("Origin");
            }
        }

        private string destination;
        public string Destination
        {
            get { return destination; }
            set
            {
                destination = value;
                OnPropertyChanged("Destination");
            }
        }

        private decimal tripCost;
        public decimal TripCost
        {
            get { return tripCost; }
            set
            {
                tripCost = value;
                OnPropertyChanged("TripCost");
            }
        }

        private string rating;
        public string Rating
        {
            get { return rating; }
            set
            {
                rating = value;
                OnPropertyChanged("Rating");
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
