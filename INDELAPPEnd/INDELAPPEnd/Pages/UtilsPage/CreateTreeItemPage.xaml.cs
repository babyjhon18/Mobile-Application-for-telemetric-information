using INDELAPPEnd.DataViewModels;
using INDELAPPEnd.Helpers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages.UtilsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTreeItemPage : ContentPage
    {
        private int CloneCount { get; set; }
        private int CounterID { get; set; }
        private int PageType { get; set; }
        public CreateTreeItemPage()
        {
            InitializeComponent();
        }

        //0 - clone counter
        public CreateTreeItemPage(int counterID, int pageType)
        {
            InitializeComponent();
            PageType = pageType;
            CounterID = counterID;
        }

        protected override void OnAppearing()
        {
            if (PageType == 0)
            {
                labelControl.Text = "Количество копий";
                entryControl.Text = "1";
                entryControl.Keyboard = Keyboard.Numeric;
                entryControl.Focus();
            }
            base.OnAppearing();
        }

        private async void AcceptButtonClicked(object sender, EventArgs e)
        {
            try
            {
                switch (PageType)
                {
                    case 0:
                        CloneCount = Convert.ToInt32(entryControl.Text);
                        if (entryControl.Text != null && entryControl.Text != "")
                        {
                            AppRepository.Counter.Clone<Object>(Links.APICounterClone + "?counterID=" + CounterID
                                + "&count=" + CloneCount, true);
                            await Navigation.PopModalAsync(true);
                            MessagingCenter.Send(this, "CloneCounter");
                            break;
                        }
                        else
                        {
                            await Navigation.PushModalAsync(new AcceptDeclinePage("Неверный формат." +
                                 "Поле количества копий может принимать только положительные целые числа, кроме нуля.",
                                 "Ок", "", false));
                            break;
                        }
                }
            }
            catch (FormatException ex)
            {
                await Navigation.PushModalAsync(new AcceptDeclinePage("Неверный формат." +
                    "Поле количества копий может принимать только положительные целые числа, кроме нуля.", "Ок", "", false));
                return;
            }
        }

        private async void DeclineButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }
    }
}