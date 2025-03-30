using System;
using System.Collections.ObjectModel;
using System.IO;

namespace GRoupHI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }


        // Your TripDatabases instance
        private TripDatabase _tripDatabases = new TripDatabase();

        // Method to delete the active trip
        public int DeleteActiveTrip()
        {
            if (ActiveDBTrip != null)
            {
                return _tripDatabases.DeleteTrip(ActiveDBTrip.TId);
            }
            else
            {
                throw new Exception("No active trip selected.");
            }
        }


        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        // global variable to hold the selected TRIP
        public DBTrip ActiveDBTrip;
    }
}

