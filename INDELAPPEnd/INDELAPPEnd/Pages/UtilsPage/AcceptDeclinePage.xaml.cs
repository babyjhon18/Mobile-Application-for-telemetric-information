using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages.UtilsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AcceptDeclinePage : ContentPage
    {
        public bool Result { get; set; }
        private int PageType { get; set; }
        private bool ObjectDelete { get; set; }
        private bool ProfileDelete { get; set; }
        public AcceptDeclinePage()
        {
            InitializeComponent();
        }
        //acceptEdit - 0, acceptDelete - 1, dissmiss - 2


        public AcceptDeclinePage(string message, string acceptText, string declineText, bool isVisibleDeclineButton)
        {
            InitializeComponent();
            labelControl.Text = message;
            acceptButton.Text = acceptText;
            declineButton.IsVisible = false;
            if (isVisibleDeclineButton)
            {
                declineButton.Text = declineText;
                declineButton.IsVisible = isVisibleDeclineButton;
            }
        }

        private async void AcceptButtonClicked(object sender, EventArgs e)
        {
            Result = true;
            await Navigation.PopModalAsync(true);
            MessagingCenter.Send(this, "AcceptOrDecline", Result);
        }

        private async void DeclineButtonClicked(object sender, EventArgs e)
        {
            Result = false;
            MessagingCenter.Send(this, "AcceptOrDecline", Result);
            await Navigation.PopModalAsync(true);
        }
    }
}