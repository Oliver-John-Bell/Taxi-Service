namespace GRoupHI;

public partial class EmptyInboxPage : ContentPage
{
	public EmptyInboxPage()
	{
		InitializeComponent();
	}

    private void OnRefreshClicked(object sender, EventArgs e)
    {
		DisplayAlert("Inbox", "You have no new mail check back later", "Ok");
    }
}