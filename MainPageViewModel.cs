using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GRoupHI
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private TripDatabase tripDatabase;

        public MainPageViewModel()
        {
            tripDatabase = new TripDatabase();
        }

        public void SaveTrip()
        {




            // Create a new DBTrip instance with current data
            DBTrip newDBTrip = new DBTrip();

            newDBTrip.TId = TId;
            newDBTrip.Origin = Origin;
            newDBTrip.Destination = Destination;
            newDBTrip.TripCost = TripCost;
             

            // Add the new trip to the database
            tripDatabase.AddTrip(newDBTrip);

             
        }

        private int tid;
        public int TId
        {
            get { return tid; }
            set
            {
                if (value != null)
                {
                    tid = value;
                    OnPropertyChanged(nameof(TId));
                }
            }
        }

        private string origin;
        public string Origin
        {
            get { return origin; }
            set
            {
                if (value != null)
                {
                    origin = value;
                    OnPropertyChanged(nameof(Origin));
                }
            }
        }

        private string destination;
        public string Destination
        {
            get { return destination; }
            set
            {
                if (value != null)
                {
                    destination = value;
                    OnPropertyChanged(nameof(Destination));
                }
            }
        }

        private decimal tripCost;
        public decimal TripCost
        {
            get { return tripCost; }
            set
            {
                // You may want to add additional validation here if necessary
                tripCost = value;
                OnPropertyChanged(nameof(TripCost));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        // INotifyPropertyChanged implementation
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
