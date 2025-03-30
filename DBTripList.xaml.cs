using System.Windows.Input;

namespace GRoupHI
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DBTripList : ContentPage
    {
        TripDatabase tripDatabase;
        DBTripListViewModel model;

        public DBTripList()
        {
            InitializeComponent();
            model = new DBTripListViewModel();
            tripDatabase = new TripDatabase(); // Initialize tripDatabase here
            BindingContext = model;

            //tripListView.ItemsSource = tripDatabase.GetTrips();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //System.Diagnostics.Debug.WriteLine($"test: {model.AllTrips[4].TId}");
            // Load trips when the page appear
            await GetTrips();
        }

        private async Task GetTrips()
        {
            // Check if tripDatabase is null before using it
            if (tripDatabase != null)
            {
                var trips =  tripDatabase.GetTrips();

                // Display the first trip's details (you can iterate through all trips if needed)
                //if (trips.Count == 0)
                if (model.AllTrips.Count == 0)
                    {
                    // Handle the case where no trips are found in the database
                    await DisplayAlert("No Trips", "No trips found in the database.", "OK");
                }
                else
                {
                    // Set the ItemsSource of tripListView to display the trips
                    tripListView.ItemsSource = trips;
                }
            }
            else
            {
                // Handle the case where tripDatabase is not initialized
                // Display an error message or handle it appropriately
                // For example:
                await DisplayAlert("Error", "Trip database is not initialized.", "OK");
            }
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                // Get the App instance
                var app = Application.Current as App;

                // Set the active trip
                app.ActiveDBTrip = (DBTrip)e.SelectedItem;

                // Pass the selected DBTrip to the constructor of CompleteTrip page
                await Navigation.PushAsync(new CompleteTrip((DBTrip)e.SelectedItem));
            }
        }





    }
}
