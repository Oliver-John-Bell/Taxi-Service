using System.Collections.ObjectModel;

namespace GRoupHI;

public partial class CompleteTrip : ContentPage
{

    CompleteTripViewModel viewModel;

    public CompleteTrip(DBTrip selectedTrip)
    {
        InitializeComponent();

        viewModel = new CompleteTripViewModel(selectedTrip);
        BindingContext = viewModel;
    }

    private void ButtonDel_Clicked(object sender, EventArgs e)
    {
        // Delete the selected trip
        viewModel.DeleteTrip();

        // Display a success message
        DisplayAlert("Success", "The trip has been deleted.", "OK");
        Navigation.PopAsync();
    }


    private void btnUpdate_Clicked(object sender, EventArgs e)
    {
        if (viewModel != null)
        {
            viewModel.UpdateTrip();
            Navigation.PopAsync();
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }
}