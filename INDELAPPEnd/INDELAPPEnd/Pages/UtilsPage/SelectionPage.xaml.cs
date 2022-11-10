using INDELAPPEnd.DataViewModels;
using INDELAPPEnd.Helpers;
using INDELLAPPEnd.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages.UtilsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectionPage : ContentPage
    {
        private int CloneCount { get; set; }
        private int CloneType { get; set; }
        private string CloneCondition { get; set; }
        private int ObjectID { get; set; }
        public SelectionPage()
        {
            InitializeComponent();
        }

        public SelectionPage(int objectID)
        {
            InitializeComponent();
            CloneType = 0;
            ObjectID = objectID;
        }

        protected override void OnAppearing()
        {
            conditionLabel.Text = "Имя объекта";
            conditionEntry.Placeholder = "Имя объекта";
            name_Original.IsChecked = true;
            conditionEntry.IsEnabled = false;
            conditionFrame.BackgroundColor = Color.FromHex("#ededed");
            cloneCountEntry.Keyboard = Keyboard.Numeric;
            base.OnAppearing();
        }

        private async void AcceptButtonClicked(object sender, EventArgs e)
        {
            try
            {
                CloneCount = Convert.ToInt32(cloneCountEntry.Text);
                CloneCondition = conditionEntry.Text;
                switch (CloneType)
                {
                    case 0:
                        if (cloneCountEntry.Text != null && cloneCountEntry.Text != "" && CloneType == 0)
                        {
                            AppRepository.Object.Clone<Object>(Links.APIObjectClone + "?objectID=" + ObjectID
                                + "&count=" + CloneCount + "&cloneType=" + CloneType, true);
                            Application.Current.MainPage = new NavigationPage(new ProfileSettingsPage(true));
                            break;
                        }
                        else
                        {
                            await Navigation.PushModalAsync(new AcceptDeclinePage("Неверный формат." +
                                "Поле количества копий может принимать только положительные целые числа, кроме нуля.",
                                "Ок", "", false));
                            break;
                        }
                    case 1:
                        if (conditionEntry.Text != null && conditionEntry.Text != "" && CloneType == 1)
                        {
                            AppRepository.Object.Clone<Object>(Links.APIObjectClone + "?objectID=" + ObjectID
                                + "&count=" + CloneCount + "&cloneType=" + CloneType + "&cloneOptions=" + CloneCondition, true);
                            Application.Current.MainPage = new NavigationPage(new ProfileSettingsPage(true));
                            break;
                        }
                        else
                        {
                            await Navigation.PushModalAsync(new AcceptDeclinePage("Ошибка заполнения." +
                                "Поле имени объекта не может быть пустым.", "Ок", "", false));
                            break;
                        }
                    case 2:
                        if (conditionEntry.Text != null && conditionEntry.Text != "" && CloneType == 2)
                        {
                            AppRepository.Object.Clone<Object>(Links.APIObjectClone + "?objectID=" + ObjectID
                                + "&count=" + CloneCount + "&cloneType=" + CloneType + "&cloneOptions=" + CloneCondition, true);
                            Application.Current.MainPage = new NavigationPage(new ProfileSettingsPage(true));
                            break;
                        }
                        else
                        {
                            await Navigation.PushModalAsync(new AcceptDeclinePage("Ошибка заполнения." +
                                "Поле адреса RTU не может быть пустым.", "Ок", "", false));
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

        private void CloneType_CheckChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton cloneTypeRadioButton = sender as RadioButton;
            switch (cloneTypeRadioButton.Content)
            {
                case "Название оригинала + '_копия'":
                    CloneType = 0;
                    conditionLabel.Text = "Имя объекта";
                    conditionEntry.Placeholder = "Имя объекта";
                    conditionEntry.IsEnabled = false;
                    conditionFrame.BackgroundColor = Color.FromHex("#ededed");
                    cloneCountEntry.Keyboard = Keyboard.Telephone;
                    cloneCountEntry.Keyboard = Keyboard.Numeric;
                    break;
                case "RTU + Адрес RTU":
                    CloneType = 2;
                    conditionLabel.Text = "RTU";
                    conditionEntry.IsEnabled = true;
                    conditionEntry.Placeholder = "Адрес RTU";
                    conditionEntry.Keyboard = Keyboard.Numeric;
                    conditionFrame.BackgroundColor = Color.Transparent;
                    cloneCountEntry.Keyboard = Keyboard.Numeric;
                    break;
                case "Задать название объекта":
                    CloneType = 1;
                    conditionLabel.Text = "Имя объекта";
                    conditionEntry.IsEnabled = true;
                    conditionEntry.Placeholder = "Имя объекта";
                    conditionEntry.Keyboard = Keyboard.Default;
                    conditionFrame.BackgroundColor = Color.Transparent;
                    cloneCountEntry.Keyboard = Keyboard.Numeric;
                    break;
            }
        }
    }
}