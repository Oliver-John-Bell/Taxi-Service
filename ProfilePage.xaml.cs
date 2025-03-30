using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
namespace GRoupHI;

public partial class ProfilePage : ContentPage{

    private TripDatabase _tripDatabase;

    private ProfileViewModel _viewModel;
    //MainPage mainPage;

    public ProfilePage()
    {
        InitializeComponent();
        _tripDatabase = new TripDatabase();
        _viewModel = new ProfileViewModel(_tripDatabase);
        BindingContext = _viewModel;
    }

   

    private void SaveLocation_Clicked(object sender, EventArgs e)
    {
        //string location = locationEntry.Text;

        // Check if the entered location is not empty
        
            // Optionally, you can display a message or perform any other action to indicate that the location has been saved
            DisplayAlert("Success", "Location saved successfully.", "OK");

            Navigation.PushAsync(new MainPage());
        
    }

    


    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Get the trip count and update progress bar color accordingly
        if (BindingContext is ProfileViewModel viewModel)
        {
            int tripCount = viewModel.TripCount;

            // Calculate points based on the trip count convention (1 point for every 10 trips)
            int points = tripCount / 10;

            // Update the Points property on the ViewModel
            viewModel.Points = points;

            if (tripCount >= 90 && tripCount < 100)
            {
                // Change progress bar color to warning color when nearing completion
                progressBar.ProgressColor = (Color)Resources["ProgressBarColor_Warning"];
            }
            else
            {
                // Reset progress bar color to normal color
                progressBar.ProgressColor = (Color)Resources["ProgressBarColor_Normal"];
            }
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new EmptyInboxPage());
    }
}