using INDELAPPEnd.Helpers;
using INDELLAPPEnd.Models;
using INDELLAPPEnd.Pages;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public string PrimaryColor => "#337ab7";

        protected override void OnAppearing()
        {
            availibleNetList.IsEnabled = true;
            base.OnAppearing();
        }
        public SettingsPage()
        {
            InitializeComponent();
            Title = "Настройки соединения";
            if (Settings.profileSettingsList.Count == 0)
            {
                availibleNetList.IsVisible = false;
                hintAvailible.IsVisible = true;
            }
            else
            {
                availibleNetList.IsVisible = true;
                hintAvailible.IsVisible = false;
            }
            if (Settings.CurrentNetworkName == "")
            {
                currentNet.IsVisible = false;
            }
            else
            {
                currentNet.IsVisible = true;
                hintAvailible.IsVisible = false;
                SetCurrentNet(
                new ProfileSettingsModel()
                {
                    Name = Settings.CurrentNetworkName,
                    Server = Settings.CurrentServer,
                    Port = Settings.CurrentPort,
                    UserName = Settings.CurrentUserName,
                    Password = Settings.CurrentPassword,
                    Claims = Settings.claims
                });
            }
            availibleNetList.ItemsSource = Settings.profileSettingsList;
            BindingContext = this;
        }
        //функция удаления сети из списка
        private void RemoveFromAvailible(ProfileSettingsModel profile)
        {
            Settings.profileSettingsList.Remove(profile);
            Sort();
            availibleNetList.ItemsSource = Settings.profileSettingsList;
        }
        //функция добавления сети в список доступных
        private void AddToAvailible(ProfileSettingsModel profile)
        {
            Settings.profileSettingsList.Add(profile);
            Sort();
            availibleNetList.ItemsSource = Settings.profileSettingsList;
        }
        //сортировка списка сетей
        private void Sort()
        {
            Settings.profileSettingsList = new ObservableCollection<ProfileSettingsModel>(Settings.
                profileSettingsList.OrderBy(item => item.Name));
            Settings.ProfilleSettingsListExtensionSerialize();
        }
        //функция кнопки добавления сети 
        private void AddConfigurationButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<EditConfigurationPage, ProfileSettingsModel>(this, "Add", (page, profile) =>
            {
                //если switch переключен в режим по умолчанию, то задаем текущее подключение
                if ((page.BindingContext as EditConfigurationPage).ByDefault)
                {
                    //если текущее подключение уже существует, то добавляем это текущее подключение в список доступных
                    if (Settings.CurrentNetworkName != "")
                    {
                        //добавляем в список доступных
                        AddToAvailible(new ProfileSettingsModel()
                        {
                            Name = Settings.CurrentNetworkName,
                            Server = Settings.CurrentServer,
                            Port = Settings.CurrentPort,
                            UserName = Settings.CurrentUserName,
                            Password = Settings.CurrentPassword,
                            Claims = Settings.claims,
                        });
                        //делаем видимость доступных сетей
                        availibleNetList.IsVisible = true;
                        //прячем подсказку об отсутствии доступных сетей
                        hintAvailible.IsVisible = false;
                    }
                    //создаем текущее соединение
                    SetCurrentNet(profile);
                    //прячем подсказку об отсутствии доступных сетей
                    hintAvailible.IsVisible = false;
                    MessagingCenter.Unsubscribe<EditConfigurationPage, ProfileSettingsModel>(this, "Add");
                }
                else
                {
                    //делаем видимым список доступных сетей
                    availibleNetList.IsVisible = true;
                    //прячем подсказку об отсутствии доступных сетей
                    hintAvailible.IsVisible = false;
                    //добавляем сеть 
                    AddToAvailible(profile);
                    MessagingCenter.Unsubscribe<EditConfigurationPage, ProfileSettingsModel>(this, "Add");
                };
            });
            Navigation.PushAsync(new EditConfigurationPage());
        }

        //клик на любую сеть из списка
        public ICommand EditClick
        {
            get
            {
                return new Command(item =>
                {
                    availibleNetList.IsEnabled = false;
                    Frame frame = item as Frame;
                    ProfileSettingsModel profile = frame.BindingContext as ProfileSettingsModel;
                    //редактирование 
                    MessagingCenter.Subscribe<EditConfigurationPage, ProfileSettingsModel>(this, "Edit", (page, choosenProfile) =>
                    {
                        //если switch переключен в режим по умолчанию, то делаем выбранную сеть текущей
                        if ((page.BindingContext as EditConfigurationPage).ByDefault)
                        {
                            //если текущая сеть не пустая, то добавляем предшествующую текущую сеть в список доступных сетей
                            if (Settings.CurrentNetworkName != "")
                            {
                                AddToAvailible(new ProfileSettingsModel()
                                {
                                    Name = Settings.CurrentNetworkName,
                                    Server = Settings.CurrentServer,
                                    Port = Settings.CurrentPort,
                                    UserName = Settings.CurrentUserName,
                                    Password = Settings.CurrentPassword,
                                    Claims = Settings.claims
                                });
                            }
                            //удаляем из списка доступных выбранную сеть
                            RemoveFromAvailible(profile);
                            //устанавливаем текущую сеть 
                            SetCurrentNet(choosenProfile);
                            MessagingCenter.Unsubscribe<EditConfigurationPage, ProfileSettingsModel>(this, "Edit");
                            Application.Current.MainPage = new NavigationPage(new ProfileSettingsPage());
                        }
                        else
                        {
                            if (Settings.CurrentNetworkName == choosenProfile.Name)
                            {
                                currentNet.IsVisible = false;
                                Settings.CurrentNetworkName = "";
                                Settings.CurrentServer = "";
                                Settings.CurrentPort = "";
                                Settings.CurrentUserName = "";
                                Settings.CurrentPassword = "";
                                Settings.CurrentClaims = "";
                                Application.Current.MainPage = new NavigationPage(new ProfileSettingsPage(true));
                            }
                            //удаляем выбранную сеть из доступных сетей 
                            RemoveFromAvailible(profile);
                            //добавляем в список доступных измененную сеть
                            AddToAvailible(choosenProfile);
                            MessagingCenter.Unsubscribe<EditConfigurationPage, ProfileSettingsModel>(this, "Edit");
                        }
                    });
                    //удаление 
                    MessagingCenter.Subscribe<EditConfigurationPage, ProfileSettingsModel>(this, "Delete", (page, choosenProfile) =>
                    {
                        //если стоит флаг по умолчанию, то сначала назначаем измененную сеть на статус текущей
                        if ((page.BindingContext as EditConfigurationPage).ByDefault)
                        {
                            SetCurrentNet(choosenProfile);
                            //затем убираем ее из списка доступных, если она там
                            RemoveFromAvailible(choosenProfile);
                            //если выбрана текущая сеть, то очищаем поля текущей сети от значений
                            if (Settings.CurrentNetworkName == choosenProfile.Name)
                            {
                                Settings.CurrentNetworkName = "";
                                Settings.CurrentServer = "";
                                Settings.CurrentPort = "";
                                Settings.CurrentUserName = "";
                                Settings.CurrentPassword = "";
                                Settings.CurrentClaims = "";
                                //и прячем форму
                                currentNet.IsVisible = false;
                                Application.Current.MainPage = new NavigationPage(new ProfileSettingsPage(false));
                            }
                            MessagingCenter.Unsubscribe<EditConfigurationPage, ProfileSettingsModel>(this, "Delete");
                        }
                        else
                        {
                            //простое удаление
                            RemoveFromAvailible(profile);
                            Navigation.PopAsync();
                            MessagingCenter.Unsubscribe<EditConfigurationPage, ProfileSettingsModel>(this, "Delete");
                        }
                        //если после удаления больше нет сетей(текущей, доступной), то выводим подсказку о том, что нет доступных сетей
                        if (Settings.profileSettingsList.Count == 0 && Settings.CurrentNetworkName == "")
                        {
                            Application.Current.MainPage = new NavigationPage(new ProfileSettingsPage(false));
                        }
                        MessagingCenter.Unsubscribe<EditConfigurationPage, ProfileSettingsModel>(this, "Delete");
                    });
                    Navigation.PushAsync(new EditConfigurationPage(profile));
                });
            }
        }
        //функция установки текущей сети
        private async void SetCurrentNet(ProfileSettingsModel profile)
        {
            currentNet.IsVisible = true;
            loadingActivityIndicator.IsEnabled = true;
            loadingActivityIndicator.IsRunning = true;
            loadingActivityIndicator.IsVisible = true;
            Settings.CurrentNetworkName = profile.Name;
            Settings.CurrentServer = profile.Server;
            Settings.CurrentPort = profile.Port;
            Settings.CurrentUserName = profile.UserName;
            Settings.CurrentPassword = profile.Password;
            Settings.claims = profile.Claims;
            Settings.ClaimsListExtensionSerialize();
            currentNet.BindingContext = profile;
            await Task.Delay(1200);
            loadingActivityIndicator.IsEnabled = false;
            loadingActivityIndicator.IsRunning = false;
            loadingActivityIndicator.IsVisible = false;
        }
    }
}