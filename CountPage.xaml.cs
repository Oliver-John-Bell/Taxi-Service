using System.Timers;

namespace GRoupHI;

public partial class CountPage : ContentPage
{
    private int _timeInSeconds;
    private System.Timers.Timer _countdownTimer;
    public MainPage _mainPage;
    public int _estimatedTimeDriver;
    public readonly TripDatabase tripDatabase = new TripDatabase();
    private DBTrip completetrip;
    private double _tripCost;
    private string _origin;
    private string _destination;


    public CountPage(int timeInSeconds, MainPage mainPage, int estimatedTimeDriver, double tripCost,string origin,string destination)
    {
        InitializeComponent();
        _timeInSeconds = timeInSeconds;
        _mainPage = mainPage;
        _estimatedTimeDriver = estimatedTimeDriver;
        _tripCost = tripCost;
        _origin = origin;
        _destination = destination;
        _countdownTimer = new System.Timers.Timer(1000); // Set interval to 1 second
        _countdownTimer.Elapsed += OnTimerElapsed;
        _countdownTimer.AutoReset = true;        
        StartCountdown();
    }

    private void StartCountdown()
    {
        UpdateCountdownDisplay();
        _countdownTimer.Start();
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        _estimatedTimeDriver--;
        // Update the UI
        MainThread.BeginInvokeOnMainThread(() => { UpdateCountdownDisplay(); });
        if (_estimatedTimeDriver <= 0)
        {
            _countdownTimer.Stop();
            // navigation?
        }
    }

    private void UpdateCountdownDisplay()
    {
        int minutes = _estimatedTimeDriver / 60;
        int seconds = _estimatedTimeDriver % 60;
        CountdownLabel.Text = $"{minutes:00}:{seconds:00}";
    }


    private async void OnCancelClicked(object sender, EventArgs e)
    {
        _countdownTimer.Stop();
        await Navigation.PopAsync();
    }

    private async void OnTripCompletedClicked(object sender, EventArgs e)
    {
        DBTrip trip = completetrip;
        await Navigation.PushAsync(new DBTripList());         
    }
}
