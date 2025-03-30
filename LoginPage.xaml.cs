using Microsoft.Maui.Controls;
namespace GRoupHI;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    { 
            // Navigate to profile page
            await Navigation.PushAsync(new ProfilePage());
    }
}