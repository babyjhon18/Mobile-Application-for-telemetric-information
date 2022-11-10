using INDELAPPEnd.DataViewModels;
using INDELAPPEnd.Helpers;
using INDELAPPEnd.Pages;
using INDELAPPEnd.Pages.UtilsPage;
using INDELLAPPEnd.Models;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELLAPPEnd.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileSettingsPage : ContentPage, IExceptionHandler
    {
        private bool _isConfigured;
        public bool IsConfigured
        {
            get { return _isConfigured; }
            set
            {
                _isConfigured = value;
                OnPropertyChanged("IsConfigured");
            }
        }


        private bool _isBusy = true;
        public bool IsBusyActivityIndicator
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusyActivityIndicator");
            }
        }
        private int MessageCounter { get; set; }
        private ClaimsModel UserClaims { get; set; }
        private ObservableCollection<CustomRegionClass> Regions { get; set; }
        public ProfileSettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessageCounter = 0;
            base.OnAppearing();
        }

        public ProfileSettingsPage(bool Configured)
        {
            InitializeComponent();
            versionLabel.Text = "Версия " + VersionTracking.CurrentVersion;
            if (Configured)
            {
                loadingIndicator.IsEnabled = Configured;
                loadingIndicator.IsRunning = Configured;
                loadingIndicator.IsVisible = Configured;
                IsConfigured = !Configured;
                Initialize();
            }
            else
            {
                loadingIndicator.IsEnabled = Configured;
                loadingIndicator.IsRunning = Configured;
                loadingIndicator.IsVisible = Configured;
                IsConfigured = !Configured;
            }
            BindingContext = this;
        }

        private async void Initialize()
        {
            await Task.Delay(2000);
            if (Settings.CurrentNetworkName != "")
            {
                UpdateLogin();
                LoadRegions();
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new SettingsPage());
            }
        }

        private void UpdateLogin()
        {
            var userClaims = AppRepository.User.Create<ClaimsModel>(Links.APILogin,
                new { username = Settings.CurrentUserName, password = Settings.CurrentPassword }, false);
            SetMessage(userClaims.Key, userClaims.Value);
        }

        private void LoadRegions()
        {
            var regions = AppRepository.Region.View(Links.APIObjectTreeGet,
                new ObservableCollection<CustomRegionClass>(), true);
            SetMessage<ObservableCollection<CustomRegionClass>>(regions.Key, regions.Value);
        }

        private void NextButtonClick(object sender, System.EventArgs e)
        {
            MessagingCenter.Subscribe<EditConfigurationPage, ProfileSettingsModel>(this, "Add", (page, profile) =>
            {
                //если switch переключен в режим по умолчанию, то задаем текущее подключение
                if ((page.BindingContext as EditConfigurationPage).ByDefault)
                {
                    Settings.CurrentNetworkName = profile.Name;
                    Settings.CurrentServer = profile.Server;
                    Settings.CurrentPort = profile.Port;
                    Settings.CurrentUserName = profile.UserName;
                    Settings.CurrentPassword = profile.Password;
                    Settings.claims = profile.Claims;
                    Settings.ClaimsListExtensionSerialize();
                    MessagingCenter.Unsubscribe<EditConfigurationPage, ProfileSettingsModel>(this, "Add");
                }
                else
                {
                    //если выключен, то добавляем в список доступных сетей
                    Settings.profileSettingsList.Add(profile);
                    MessagingCenter.Unsubscribe<EditConfigurationPage, ProfileSettingsModel>(this, "Add");
                };

            });
            Navigation.PushAsync(new EditConfigurationPage(true));
        }

        public void SetMessage<T>(HttpStatusCode code, T data)
        {
            MessageCounter += 1;
            if (data is ObservableCollection<CustomRegionClass> && data != null)
            {
                Regions = data as ObservableCollection<CustomRegionClass>;
                Application.Current.MainPage = new NavigationPage(new RegionsPage(Regions));
            }
            else if (data is ClaimsModel && data != null)
            {
                UserClaims = data as ClaimsModel;
                Settings.AccessToken = UserClaims.Access_token;
                Settings.IsAdmin = UserClaims.IsAdmin;
                Settings.claims = UserClaims.Claims;
                Settings.ClaimsListExtensionSerialize();
            }
            else if (MessageCounter == 1)
            {
                MessagingCenter.Subscribe<AcceptDeclinePage, bool>(this, "AcceptOrDecline", (page, boolItem) =>
                {
                    if (page.Result)
                        Application.Current.MainPage = new ProfileSettingsPage(true);
                    else
                        Application.Current.MainPage = new NavigationPage(new SettingsPage());
                    MessagingCenter.Unsubscribe<AcceptDeclinePage, bool>(this, "AcceptOrDecline");
                });
                Navigation.PushModalAsync(new AcceptDeclinePage("Не удалось подключиться к серверу.",
                    "Попробовать снова", "Настройки", true));
            }
        }
    }
}