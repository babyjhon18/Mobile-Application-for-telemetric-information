using INDELLAPPEnd.Models;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace INDELAPPEnd.Helpers
{
    public static class Settings
    {
        public static ISettings AppSettings => CrossSettings.Current;
        public static bool IsAdmin
        {
            get => AppSettings.GetValueOrDefault<bool>("IsAdmin", false);
            set => AppSettings.AddOrUpdateValue<bool>("IsAdmin", value);
        }
        public static bool NeedAccessToken
        {
            get => AppSettings.GetValueOrDefault<bool>("NeedAccessToken", false);
            set => AppSettings.AddOrUpdateValue<bool>("NeedAccessToken", value);
        }
        public static string AccessToken
        {
            get => AppSettings.GetValueOrDefault<string>("AccessToken", "");
            set => AppSettings.AddOrUpdateValue<string>("AccessToken", value);
        }
        public static string CurrentNetworkName
        {
            get => AppSettings.GetValueOrDefault<string>("CurrentNetworkName", "");
            set => AppSettings.AddOrUpdateValue<string>("CurrentNetworkName", value);
        }
        public static string CurrentUserName
        {
            get => AppSettings.GetValueOrDefault<string>("CurrentUserName", "");
            set => AppSettings.AddOrUpdateValue<string>("CurrentUserName", value);
        }
        public static string CurrentPassword
        {
            get => AppSettings.GetValueOrDefault<string>("CurrentPassword", "");
            set => AppSettings.AddOrUpdateValue<string>("CurrentPassword", value);
        }
        public static string CurrentServer
        {
            get => AppSettings.GetValueOrDefault<string>("CurrentServer", "");
            set => AppSettings.AddOrUpdateValue<string>("CurrentServer", value);
        }
        public static string CurrentPort
        {
            get => AppSettings.GetValueOrDefault<string>("CurrentPort", "");
            set => AppSettings.AddOrUpdateValue<string>("CurrentPort", value);
        }
        public static string CurrentClaims
        {
            get => AppSettings.GetValueOrDefault<string>("CurrentClaims", "");
            set => AppSettings.AddOrUpdateValue<string>("CurrentClaims", value);
        }
        public static string ProfileSettingsString
        {
            get => AppSettings.GetValueOrDefault<string>("ProfileSettingsString", "");
            set => AppSettings.AddOrUpdateValue<string>("ProfileSettingsString", value);
        }

        public static List<KeyValuePair<string, string>> claims;
        public static void ClaimsListExtensionSerialize()
        {
            CurrentClaims = JsonConvert.SerializeObject(claims);
        }

        public static void ClaimsListExtensionDeserialize()
        {
            claims = JsonConvert.
                DeserializeObject<List<KeyValuePair<string, string>>>(CurrentClaims.ToString());
            if (claims == null)
                claims = new List<KeyValuePair<string, string>>();
        }

        public static ObservableCollection<ProfileSettingsModel> profileSettingsList;
        public static void ProfilleSettingsListExtensionSerialize()
        {
            ProfileSettingsString = JsonConvert.SerializeObject(profileSettingsList);
        }
        public static void ProfilleSettingsListExtensionDeserialize()
        {
            profileSettingsList = JsonConvert.
                DeserializeObject<ObservableCollection<ProfileSettingsModel>>(ProfileSettingsString.ToString());
            if (profileSettingsList == null)
                profileSettingsList = new ObservableCollection<ProfileSettingsModel>();
        }
    }
}
