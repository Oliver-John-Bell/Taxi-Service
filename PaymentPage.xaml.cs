using System.Xml;

namespace GRoupHI;

public partial class PaymentPage : ContentPage
{
    private int timeInSeconds;
    MainPage mainPage;
    public int _estimatedTimeDriver;
    public double _tripCost;
    public string _origin;
    public string _destination;


    public PaymentPage(MainPage mainPage, int estimatedTimeDriver, double tripCost, string origin, string destination)
    {
        InitializeComponent();
        this.mainPage = mainPage;
        _estimatedTimeDriver = estimatedTimeDriver;
        _destination = destination;
        _tripCost = tripCost;
        _origin = origin;
    }
    public bool Verify(string cardNumber) // luhn algorithm used to check 
    {
        int? sum = 0;
        bool alternate = false;
        char[] chars = cardNumber.ToCharArray();
        Array.Reverse(chars);
        for (int i = 0; i < chars.Length; i++)
        {
            int digit = chars[i] - '0'; // convert to int
            if (alternate)
            {
                digit *= 2; // Double every second digit
                if (digit > 9) { digit -= 9; } // If the doubled value is greater than 9, subtract 9
            }
            sum += digit; // Add the digit to the total sum
            alternate = !alternate;
        }
        return (sum % 10 == 0); // If the total sum is a multiple of 10, the number is valid
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        bool error = false;
        string? msg = "";
        if (CardNumberEntry.Text == null || CardNumberEntry.Text.Length != 16)
        {
            error = true;
            msg += "Card number must be 13 digits, ";
            CardNumberLabel.TextColor = Color.FromRgb(255, 0, 0);
        }
        else if (!Verify(CardNumberEntry.Text))
        {
            error = true;
            msg += "Invalid card number, ";
            CardNumberLabel.TextColor = Color.FromRgb(255, 0, 0);
        }
        if (NameEntry.Text == null || NameEntry.Text == "")
        {
            error = true;
            msg += "Name required, ";
            NameLabel.TextColor = Color.FromRgb(255, 0, 0);
        }
        if (!verify())
        {
            error = true;
            msg += "Expired date, ";
            ExpirationDateLabel.TextColor = Color.FromRgb(255, 0, 0);
        }
        if (CCVEntry.Text == null || CCVEntry.Text.Length != 3)
        {
            error = true;
            msg += "CCV must be 3 digits";
            CCVLabel.TextColor = Color.FromRgb(255, 0, 0);
        }
        if (error) { DisplayAlert("Error", msg, "ok"); }
        else
        {
            CardNumberLabel.TextColor = Color.FromRgb(255, 255, 255);
            NameLabel.TextColor = Color.FromRgb(255, 255, 255);
            ExpirationDateLabel.TextColor = Color.FromRgb(255, 255, 255);
            CCVLabel.TextColor = Color.FromRgb(255, 255, 255);
            toCountPage();
        }
    }

    public bool verify()
    {
        if (int.TryParse(ExpirationMonthEntry.Text, out int expMonth) && int.TryParse(ExpirationYearEntry.Text, out int expYear))
        {
            var lastDayOfMonth = DateTime.DaysInMonth(expYear, expMonth);
            var expirationDate = new DateTime(expYear, expMonth, lastDayOfMonth);
            return expirationDate > DateTime.Now;
        }
        return false;
    }

    public async void toCountPage()
    {
        // Provide the required parameters when navigating to CountPage
        await Navigation.PushAsync(new CountPage(timeInSeconds, mainPage, _estimatedTimeDriver, _tripCost, _origin, _destination));
    }

}