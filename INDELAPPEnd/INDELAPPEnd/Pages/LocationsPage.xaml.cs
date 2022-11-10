using INDELLAPPEnd.Models;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationsPage : ContentPage
    {
        public CustomRegionClass CurrentRegion { get; set; }
        private ToolbarItem SettingsToolbarItem { get; set; }
        public ObservableCollection<CustomRegionClass> Regions = new ObservableCollection<CustomRegionClass>();
        public ObservableCollection<CustomLocationClass> Locations = new ObservableCollection<CustomLocationClass>();
        public LocationsPage(CustomRegionClass region, ObservableCollection<CustomRegionClass> regions)
        {
            InitializeComponent();
            CurrentRegion = region;
            locationList.ItemsSource = region.Locations;
            Regions = regions;
            Title = region.name;
            SettingsToolbarItem = new ToolbarItem()
            {
                Text = "Настройки",
                Order = ToolbarItemOrder.Secondary
            };
            SettingsToolbarItem.Clicked += SettingsButtonClick;
            ToolbarItems.Insert(1, SettingsToolbarItem);
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            locationList.IsEnabled = true;
            base.OnAppearing();
        }

        private async void SelectedLocation(object sender, ItemTappedEventArgs e)
        {
            locationList.IsEnabled = false;
            var selectedLocation = e.Item as CustomLocationClass;
            await Navigation.PushAsync(new ObjectsPage(selectedLocation, Regions), true);
        }

        private async void SearchButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage(Regions), true);
        }

        private async void SettingsButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(), true);
        }
    }
}