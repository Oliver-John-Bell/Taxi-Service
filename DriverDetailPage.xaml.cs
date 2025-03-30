using System.Linq;

namespace GRoupHI;

public partial class DriverDetailPage : ContentPage
{
    private Driver _driver;
    private readonly string _origin;
    private readonly string _destination;
    public int _estimatedTimeDriver;
    private readonly int _estimatedTimeTrip;
    private double _tripCost;
    MainPage mainPage;

    public DriverDetailPage(Driver driver,string origin, string destination, int estimatedTimeDriver, int estimatedTimeTrip, double tripCost)
	{
		InitializeComponent();
        _driver = driver;
        _origin = origin;
        _destination = destination;
        _estimatedTimeDriver = estimatedTimeDriver;
        _estimatedTimeTrip = estimatedTimeTrip;
        _tripCost = tripCost;
        DriverImage.Source = _driver.ImageSource;
        DriverName.Text = _driver.Name;
        CarModel.Text = _driver.CarModel;
        Rating.Text = $"Rating: {_driver.Rating:F1}";
        ETAC.Text = $"Your driver till arrive in: {_estimatedTimeDriver} minutes";
        ETAG.Text = $"Your trip will take: {_estimatedTimeTrip} minutes";
    }

    private async void OnConfirmClicked(object sender, EventArgs e)
    {
        string message = $"Your driver's number is {_driver.DId}. The driver will arrive in {_estimatedTimeDriver} minutes.";

        // Show the message to the user
        await DisplayAlert("Trip Confirmation", message, "OK");

        await Navigation.PushAsync(new PaymentPage(mainPage, _estimatedTimeDriver, _tripCost, _destination, _origin));
    }


}