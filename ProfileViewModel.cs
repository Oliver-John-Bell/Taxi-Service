using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRoupHI
{
    public class ProfileViewModel : BindableObject
    {
        private TripDatabase _tripDatabase;
        private int _tripCount;

        public int TripCount
        {
            get => _tripCount;
            set
            {
                _tripCount = value;
                OnPropertyChanged();
            }
        }

        private int _points; // Private field for the points property

        public int Points // Public property for points
        {
            get { return _points; }
            set
            {
                if (_points != value)
                {
                    _points = value;
                    OnPropertyChanged(nameof(Points)); // Notify UI of property change
                }
            }
        }

        
        public ProfileViewModel(TripDatabase tripDatabase)
        {
            _tripDatabase = tripDatabase;
            // Initialize TripCount from TripDatabase
            TripCount = _tripDatabase.GetTrips().Count;
        }
    }
}
