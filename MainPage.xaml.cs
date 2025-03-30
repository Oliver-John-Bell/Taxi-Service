using Newtonsoft.Json.Linq;
using SQLite;
using System.Collections.ObjectModel;


namespace GRoupHI
{
    public partial class MainPage : ContentPage
    {
        private const string ApiKey = "AIzaSyCp1OvsBoU_o0IkL0c5GwnSyGXxdaRs2gE";
        public const string CommonStartPoint = "MK18 1EG";
        public Entry OriginEntry => originEntry;
        public Entry DestinationEntry => destinationEntry;
        TripDatabase tripDatabase;



        public MainPage()
        {
            InitializeComponent();

            tripDatabase = new TripDatabase();

        }

        private async void OnCalculateClicked(object sender, EventArgs e)
        {
            string origin = originEntry.Text;
            string destination = destinationEntry.Text;
            double tripCost = await CalculateTripCostAsync(origin, destination);

            double distanceToOrigin = await GetDistanceAsync(CommonStartPoint, origin);
            int estimatedTimeToOrigin = EstimateTimeFromDistance(distanceToOrigin);

            double distanceInMeters = await GetDistanceAsync(origin, destination);


            double distanceToDestination = await GetDistanceAsync(CommonStartPoint, destination);
            int estimatedTimeToDestination = EstimateTimeFromDistance(distanceToDestination);

            // Display trip details to the user
            string tripDetails = $"Trip Details\n\n" +
                                $"From Driver ({CommonStartPoint}) to Pick up point ({origin}):\n" +
                                $"Distance: {distanceToOrigin} meters, Estimated Time: {estimatedTimeToOrigin} minutes\n\n" +
                                $"From Origin to Destination:\n" +
                                $"Trip Cost: £{tripCost:F2}\n" +
                                $"Distance: {distanceToDestination} meters, Estimated Time: {estimatedTimeToDestination} minutes";

            await DisplayAlert("Trip Details", tripDetails, "OK");
            //await DisplayAlert("Remider", "Please click on the Save button to log your trip.", "Ok");

            var drivers = new List<Driver>
            {
                new Driver { DId = 010234, DriverPhoneNumber = 074537451 , Name = "Driver 1", CarModel = "Model 1", Rating = 4.5, ImageSource = "https://parkridgevet.com.au/wp-content/uploads/2022/06/blank-profile.jpg",  EstimatedTimeDriver = estimatedTimeToOrigin, EstimatedTimeTrip= estimatedTimeToDestination, StartPiont = CommonStartPoint},
                new Driver { DId = 010235, DriverPhoneNumber = 074537452 , Name = "Driver 2", CarModel = "Model 2", Rating = 4.7, ImageSource = "https://parkridgevet.com.au/wp-content/uploads/2022/06/blank-profile.jpg",  EstimatedTimeDriver = estimatedTimeToOrigin, EstimatedTimeTrip= estimatedTimeToDestination, StartPiont = CommonStartPoint},
                new Driver { DId = 010236, DriverPhoneNumber = 074537453 , Name = "Driver 3", CarModel = "Model 3", Rating = 4.3, ImageSource = "https://parkridgevet.com.au/wp-content/uploads/2022/06/blank-profile.jpg",  EstimatedTimeDriver = estimatedTimeToOrigin, EstimatedTimeTrip= estimatedTimeToDestination, StartPiont = CommonStartPoint},
                new Driver { DId = 010237, DriverPhoneNumber = 074537454 , Name = "Driver 4", CarModel = "Model 4", Rating = 4.2, ImageSource = "https://parkridgevet.com.au/wp-content/uploads/2022/06/blank-profile.jpg",  EstimatedTimeDriver = estimatedTimeToOrigin, EstimatedTimeTrip= estimatedTimeToDestination, StartPiont = CommonStartPoint},
                new Driver { DId = 010238, DriverPhoneNumber = 074537455 , Name = "Driver 5", CarModel = "Model 5", Rating = 4.1, ImageSource = "https://parkridgevet.com.au/wp-content/uploads/2022/06/blank-profile.jpg",  EstimatedTimeDriver = estimatedTimeToOrigin, EstimatedTimeTrip= estimatedTimeToDestination, StartPiont = CommonStartPoint},
                new Driver { DId = 010239, DriverPhoneNumber = 074537456 , Name = "Driver 6", CarModel = "Model 6", Rating = 4.9, ImageSource = "https://parkridgevet.com.au/wp-content/uploads/2022/06/blank-profile.jpg",  EstimatedTimeDriver = estimatedTimeToOrigin, EstimatedTimeTrip= estimatedTimeToDestination, StartPiont = CommonStartPoint},
            };


            DriverListView.ItemsSource = drivers;

           
            await Save();
            //saveButton.IsVisible = true;
        }

        public string GetOriginText()
        {
            return OriginEntry.Text;
        }

        public string GetDestinationText()
        {
            return DestinationEntry.Text;
        }

        public async Task<double> CalculateTripCostAsync(string origin, string destination)
        {
            try
            {
                // Construct the URL for the Google Maps Distance Matrix API request
                string apiUrl = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin}&destinations={destination}&key={ApiKey}";

                using (var client = new HttpClient())
                {
                    // Send the HTTP request to the Google Maps Distance Matrix API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    // Read the response content as JSON
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Parse the JSON response to extract distance information
                    // For simplicity, let's assume the distance is returned in meters
                    double distanceInMeters = ParseDistanceFromJson(responseBody);

                    // Calculate trip cost (for example, £2.50 per meter)
                    double tripCost = distanceInMeters * 0.0025; // Adjust the cost calculation as needed

                    // Calculate estimated time based on distance
                    int estimatedTime = EstimateTimeFromDistance(distanceInMeters);

                    // Return the calculated trip cost
                    return tripCost;
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the API request or response parsing
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");

                // If an error occurs, return a default trip cost value (0.0 in this case)
                return 0.0;
                
            }
        }

        private double ParseDistanceFromJson(string responseBody)
        {
            // Parse the JSON string into a JObject
            JObject jsonObject = JObject.Parse(responseBody);

            // Check if the API returned a valid response
            string? status = jsonObject["status"]?.ToString();
            if (status != "OK")
            {
                throw new Exception($"Google Maps API returned status: {status}");
            }

            // Extract distance value (in meters) from the JSON
            JToken? row = jsonObject["rows"]?.FirstOrDefault();
            JToken? element = row?["elements"]?.FirstOrDefault();
            string? distanceValue = element?["distance"]?["value"]?.ToString();

            if (int.TryParse(distanceValue, out int distanceInMeters))
            {
                return distanceInMeters;
            }
            else
            {
                throw new Exception("Unable to parse distance from JSON response.");
            }
        }

        public async Task<double> GetDistanceAsync(string origin, string destination)
        {
            try
            {
                // Construct the URL for the Google Maps Distance Matrix API request
                string apiUrl = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin}&destinations={destination}&key={ApiKey}";

                using (var client = new HttpClient())
                {
                    // Send the HTTP request to the Google Maps Distance Matrix API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    // Read the response content as JSON
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Parse the JSON response to extract distance information
                    // For simplicity, let's assume the distance is returned in meters
                    double distanceInMeters = ParseDistanceFromJson(responseBody);

                    // Return the extracted distance
                    return distanceInMeters;
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the API request or response parsing
                await DisplayAlert("Error", $"An error occurred while fetching distance: {ex.Message}", "OK");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

        public int EstimateTimeFromDistance(double distanceInMeters)
        {
            // Assuming an average speed of 30 meters per second (approximately 108 km/h or 67 mph)
            double averageSpeed = 30.0; // meters per second
            double timeInSeconds = distanceInMeters / averageSpeed;

            // Convert time to minutes
            int timeInMinutes = (int)Math.Round(timeInSeconds / 60);

            return timeInMinutes;
        }

        private async Task Save()
        {
            try
            {
                string origin = OriginEntry.Text;
                string destination = DestinationEntry.Text;
                decimal tripCost = (decimal)await CalculateTripCostAsync(origin, destination);

                // Create a new instance of DBTrip with trip details
                DBTrip newTrip = new DBTrip
                {
                    //TId  = tid,
                    Origin = origin,
                    Destination = destination,
                    TripCost = tripCost
                };

                // Initialize TripDatabase
                //TripDatabase tripDatabase = new TripDatabase();

                // Add the new trip to the database
                //ObservableCollection<DBTrip>  = tripDatabase.GetTrips();
                //System.Diagnostics.Debug.WriteLine($"Trip: {temp[temp.Count - 1].TId} {temp[temp.Count - 1].Destination}");
                int rowsAffected = tripDatabase.AddTrip(newTrip);

                
                //temp = tripDatabase.GetTrips();
                //System.Diagnostics.Debug.WriteLine($"Trip: {temp[temp.Count-1].TId} {temp[temp.Count - 1].Destination}");

                if (rowsAffected > 0)
                {
                    // Trip saved successfully
                    await DisplayAlert("Success", "Trip details saved to database.", "OK");
                }
                else
                {
                    // Failed to save trip
                    await DisplayAlert("Error", "Failed to save trip details.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                await DisplayAlert("Error", $"HI An error occurred: {ex.Message}", "OK");
            }
        }

        private async void DriverListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var driver = (Driver)e.Item;
            string origin = originEntry.Text;
            string destination = destinationEntry.Text;
            double tripCost = await CalculateTripCostAsync(origin, destination);
            int estimatedTimeDriver = EstimateTimeFromDistance(await GetDistanceAsync(CommonStartPoint, originEntry.Text));
            int estimatedTimeTrip = EstimateTimeFromDistance(await GetDistanceAsync(originEntry.Text, destinationEntry.Text));
            await Navigation.PushAsync(new DriverDetailPage(driver, origin, destination, estimatedTimeDriver, estimatedTimeTrip, tripCost));
        }

        private async void OnProfileAvatarClicked(object sender, EventArgs e)
        {
            ProfilePage profilePage = new ProfilePage();

            await Navigation.PushAsync(profilePage);
        }

    }
}


// Save funtion that stores everything into the database 
// creat and obtect of the trip
// call the addtrip functionbn from tripdatabase
// and the addtrip function takes a pareameter of the trip i want to have