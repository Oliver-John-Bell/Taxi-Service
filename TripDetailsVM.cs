using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace GRoupHI
{
    public class TripViewModel : INotifyPropertyChanged
    {
        private readonly TripDatabase tripDatabase;

        public ObservableCollection<DBTrip> Trips { get; set; }

        public Command DeleteTripCommand { get; set; }
        public Command RateTripCommand { get; set; }

        private DBTrip selectedTrip;
        public DBTrip SelectedTrip
        {
            get => selectedTrip;
            set
            {
                if (selectedTrip != value)
                {
                    selectedTrip = value;
                    OnPropertyChanged("SelectedTrip");
                }
            }
        }

    

      

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
