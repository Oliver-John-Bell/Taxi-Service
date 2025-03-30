using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GRoupHI
{
    public class SharedViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DBTrip> AllTrips { get; set; } = new ObservableCollection<DBTrip>();

        public event Action<DBTrip> AddedTrip;

        public void AddTrip(DBTrip trip)
        {
            AllTrips.Add(trip);
            AddedTrip?.Invoke(trip);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}