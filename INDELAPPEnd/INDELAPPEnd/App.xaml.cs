using INDELAPPEnd.Helpers;
using INDELLAPPEnd.Pages;
using Xamarin.Forms;

namespace INDELAPPEnd
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Settings.ProfilleSettingsListExtensionDeserialize();
            if (Settings.profileSettingsList.Count == 0 && Settings.CurrentNetworkName == "")
                MainPage = new NavigationPage(new ProfileSettingsPage(false));
            else
                MainPage = new NavigationPage(new ProfileSettingsPage(true));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
