using INDELAPPEnd.DataViewModels;
using INDELAPPEnd.Helpers;
using INDELAPPEnd.Pages.UtilsPage;
using INDELLAPPEnd.Models;
using INDELLAPPEnd.Pages;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditConfigurationPage : ContentPage, IExceptionHandler
    {
        private ProfileSettingsModel SelectedProfile { get; set; }
        public bool IsEdit { get; set; }
        public bool IsFirstTime { get; set; }

        private bool byDefault;
        public bool ByDefault
        {
            get { return byDefault; }
            set
            {
                byDefault = value;
                OnPropertyChanged("ByDefault");
            }
        }

        public string PrimaryColor => "#337ab7";
        public string ReversePrimaryColor => "#8c8c8c";
        public EditConfigurationPage(bool isFirstTime)
        {
            InitializeComponent();
            mainView.IsEnabled = true;
            IsFirstTime = isFirstTime;
            loadingIndicator.IsVisible = false;
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsEnabled = false;
            switchStackLayout.IsVisible = false;
            ByDefault = true;
            IsEdit = false;
            Title = "Добавить новый профиль";
            passwordEntry.IsPassword = true;
            passwordViewImage.Source = "CloseEye.png";
            BindingContext = this;
        }

        public EditConfigurationPage()
        {
            InitializeComponent();
            IsEdit = false;
            mainView.IsEnabled = true;
            loadingIndicator.IsVisible = false;
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsEnabled = false;
            Title = "Добавить новый профиль";
            passwordEntry.IsPassword = true;
            passwordViewImage.Source = "CloseEye.png";
            BindingContext = this;
        }

        public EditConfigurationPage(ProfileSettingsModel profileSettings)
        {
            InitializeComponent();
            SelectedProfile = profileSettings;
            mainView.IsEnabled = true;
            loadingIndicator.IsVisible = false;
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsEnabled = false;
            IsEdit = true;
            Title = "Редактирование профиля";
            passwordEntry.IsPassword = true;
            passwordViewImage.Source = "CloseEye.png";
            networkNameEntry.Text = profileSettings.Name;
            serverEntry.Text = profileSettings.Server;
            portEntry.Text = profileSettings.Port;
            usernameEntry.Text = profileSettings.UserName;
            passwordEntry.Text = profileSettings.Password;
            if (Settings.CurrentNetworkName == profileSettings.Name &&
                Settings.CurrentServer == profileSettings.Server &&
                Settings.CurrentPort == profileSettings.Port &&
                Settings.CurrentUserName == profileSettings.UserName &&
                Settings.CurrentPassword == profileSettings.Password)
            {
                ByDefault = true;
            }
            ToolbarItem deleteToolbarItem = new ToolbarItem()
            {
                IconImageSource = "DeleteCan.png",
                Command = Delete
            };
            ToolbarItems.Add(deleteToolbarItem);
            BindingContext = this;
        }

        public ICommand Delete
        {
            get
            {
                return new Command(() =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        MessagingCenter.Subscribe<AcceptDeclinePage, bool>(this, "AcceptOrDecline", (page, boolItem) =>
                        {
                            if (page.Result)
                            {
                                MessagingCenter.Send(this, "Delete", SelectedProfile);
                            }
                            MessagingCenter.Unsubscribe<AcceptDeclinePage, bool>(this, "AcceptOrDecline");
                        });
                        await Navigation.PushModalAsync(new AcceptDeclinePage("Подтвердить удаление?", "Ок", "Отмена", true));
                    });
                });
            }
        }

        public ICommand AcceptNewConfiguration
        {
            get
            {
                return new Command(async () =>
                {
                    mainView.IsEnabled = false;
                    loadingIndicator.IsVisible = true;
                    loadingIndicator.IsRunning = true;
                    loadingIndicator.IsEnabled = true;
                    if (CheckFields(networkNameEntry.Text))
                    {
                        await Navigation.PushModalAsync(new AcceptDeclinePage("Ошибка заполнения. " +
                            "Поле названия не может быть пустым", "Ок", "", false));
                        loadingIndicator.IsVisible = false;
                        loadingIndicator.IsRunning = false;
                        loadingIndicator.IsEnabled = false;
                        mainView.IsEnabled = true;
                        return;
                    }
                    if (CheckFields(serverEntry.Text))
                    {
                        await Navigation.PushModalAsync(new AcceptDeclinePage(
                           "Ошибка заполнения. Поле сервера не может быть пустым", "Ок", "", false));
                        loadingIndicator.IsVisible = false;
                        loadingIndicator.IsRunning = false;
                        loadingIndicator.IsEnabled = false;
                        mainView.IsEnabled = true;
                        return;
                    }
                    if (CheckFields(usernameEntry.Text))
                    {
                        await Navigation.PushModalAsync(new AcceptDeclinePage(
                           "Ошибка заполнения. Поле имени пользователя не может быть пустым", "Ок", "", false));
                        loadingIndicator.IsVisible = false;
                        loadingIndicator.IsRunning = false;
                        loadingIndicator.IsEnabled = false;
                        mainView.IsEnabled = true;
                        return;
                    }
                    if (CheckFields(passwordEntry.Text))
                    {
                        await Navigation.PushModalAsync(new AcceptDeclinePage(
                            "Ошибка заполнения. Поле пароля не может быть пустым", "Ок", "", false));
                        loadingIndicator.IsVisible = false;
                        loadingIndicator.IsRunning = false;
                        loadingIndicator.IsEnabled = false;
                        mainView.IsEnabled = true;
                        return;
                    }

                    if (IsEdit)
                    {
                        if (await NameAndDataValidation())
                        {
                            MessagingCenter.Send(this,
                                "Edit", validProfile);
                            if (ByDefault)
                                Application.Current.MainPage = new NavigationPage(new ProfileSettingsPage(true));
                            else
                                await Navigation.PopAsync();
                        }
                    }
                    else
                    {
                        if (await NameAndDataValidation())
                        {
                            MessagingCenter.Send(this,
                                "Add", validProfile);
                            if (ByDefault)
                                Application.Current.MainPage = new NavigationPage(new ProfileSettingsPage(true));
                            else
                                await Navigation.PopAsync(true);
                        }
                    }
                });
            }
        }

        private ProfileSettingsModel validProfile;
        private async Task<bool> NameAndDataValidation()
        {
            Settings.profileSettingsList.Remove(SelectedProfile);
            if (!IsUnicName(networkNameEntry.Text))
            {
                loadingIndicator.IsVisible = false;
                loadingIndicator.IsRunning = false;
                loadingIndicator.IsEnabled = false;
                mainView.IsEnabled = true;
                await Navigation.PushModalAsync(new AcceptDeclinePage("Такой профиль уже существует. " +
                    "Пожалуйста измените имя сети.", "Ок", "", false));
                return false;
            }
            await Task.Delay(1000);
            CreateProfile();
            if (validProfile == null)
                return false;
            return true;
        }

        private bool IsUnicName(string networkName)
        {
            foreach (var checkProfile in Settings.profileSettingsList)
            {
                if (checkProfile.Name == networkName)
                    return false;
            }
            return true;
        }

        public void CreateProfile()
        {
            ProfileSettingsModel profile = new ProfileSettingsModel()
            {
                Name = networkNameEntry.Text,
                Server = serverEntry.Text,
                Port = portEntry.Text,
                UserName = usernameEntry.Text,
                Password = passwordEntry.Text,
            };
            var result = AppRepository.User.Create<ClaimsModel>("http://" +
                serverEntry.Text + ":" + portEntry.Text + "/api/Login",
                                new
                                {
                                    username = profile.UserName,
                                    password = profile.Password
                                }, false);
            SetMessage(result.Key, result.Value);
        }

        private bool CheckFields(string field)
        {
            if (field != "" && field != null)
            {
                return false;
            }
            else
                return true;
        }

        public void SetMessage<T>(HttpStatusCode code, T data)
        {
            loadingIndicator.IsVisible = false;
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsEnabled = false;
            mainView.IsEnabled = true;
            switch (code)
            {
                case HttpStatusCode.OK:
                    ClaimsModel userClaims = data as ClaimsModel;
                    Settings.AccessToken = userClaims.Access_token;
                    Settings.IsAdmin = userClaims.IsAdmin;
                    validProfile = new ProfileSettingsModel()
                    {
                        Name = networkNameEntry.Text,
                        Server = serverEntry.Text,
                        Port = portEntry.Text,
                        UserName = usernameEntry.Text,
                        Password = passwordEntry.Text,
                        Claims = userClaims.Claims
                    };
                    break;
                case HttpStatusCode.Forbidden:
                    Navigation.PushModalAsync(new AcceptDeclinePage("Неверные имя пользователя или " +
                        "пароль.", "Ок", "", false));
                    break;
                case HttpStatusCode.InternalServerError:
                    Navigation.PushModalAsync(new AcceptDeclinePage("Неверный сервер. " +
                        "Проверьте строку сервера", "Ок", "", false));
                    break;
                case HttpStatusCode.NotFound:
                    Navigation.PushModalAsync(new AcceptDeclinePage("Неверный сервер." +
                        " Проверьте строку сервера", "Ок", "", false));
                    break;
                case HttpStatusCode.BadRequest:
                    Navigation.PushModalAsync(new AcceptDeclinePage("Неверный сервер. " +
                        "Проверьте строку сервера", "Ок", "", false));
                    break;
            }
        }

        private void HidenPassword(object sender, System.EventArgs e)
        {
            passwordEntry.IsPassword = passwordEntry.IsPassword ? false : true;
            if (passwordEntry.IsPassword)
                passwordViewImage.Source = "CloseEye.png";
            else
                passwordViewImage.Source = "OpenEye.png";
        }
    }
}