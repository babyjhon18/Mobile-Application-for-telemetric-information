using INDELAPPEnd.DataViewModels;
using INDELAPPEnd.Helpers;
using INDELAPPEnd.Pages.ConfigurationPageToolbarPages;
using INDELAPPEnd.Pages.ObjectPages;
using INDELLAPPEnd.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObjectsPage : ContentPage
    {
        public ObservableCollection<SimpleObjectClass> Objects = new ObservableCollection<SimpleObjectClass>();
        public CustomLocationClass CurrentLocation { get; set; }
        private ToolbarItem NewObjectToolbarItem { get; set; }
        private ToolbarItem SettingsToolbarItem { get; set; }
        private ObjectViewClass ViewObject { get; set; }

        public ObservableCollection<CustomRegionClass> Regions = new ObservableCollection<CustomRegionClass>();
        public ObjectsPage(CustomLocationClass location, ObservableCollection<CustomRegionClass> regions)
        {
            InitializeComponent();
            NewObjectToolbarItem = new ToolbarItem()
            {
                Text = "Новый объект",
                Order = ToolbarItemOrder.Secondary
            };
            NewObjectToolbarItem.Clicked += AddNewObjectButtonClicked;
            if (Settings.claims.Contains(new KeyValuePair<string, string>("Object", "Create")) || Settings.IsAdmin)
                ToolbarItems.Insert(0, NewObjectToolbarItem);
            SettingsToolbarItem = new ToolbarItem()
            {
                Text = "Настройки",
                Order = ToolbarItemOrder.Secondary
            };
            SettingsToolbarItem.Clicked += SettingsButtonClick;
            ToolbarItems.Insert(1, SettingsToolbarItem);
            CurrentLocation = location;
            objectsList.ItemsSource = location.Objects;
            Regions = regions;
            Title = location.name;
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            objectsList.IsEnabled = true;
            base.OnAppearing();
        }

        private async void SelectedObject(object sender, ItemTappedEventArgs e)
        {
            objectsList.IsEnabled = false;
            SimpleObjectClass baseObject = e.Item as SimpleObjectClass;
            await Navigation.PushAsync(new ObjectTabbedPage(baseObject.id, baseObject.name, Regions), true);
        }

        private async void SearchButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage(Regions), true);
        }

        private async void SettingsButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(), true);
        }

        private async void AddNewObjectButtonClicked(object sender, EventArgs e)
        {
            GetObject(-1);
            Task.WaitAll();
            var page = new NavigationPage(new EditObjectPage(ViewObject, 0));
            await Navigation.PushModalAsync(page, true);
        }

        private void GetObject(int objectID)
        {
            ViewObject = AppRepository.Object.
                View(Links.APIObjectGet + "?objectID=" + objectID, new List<ObjectViewClass>(), true).Value.FirstOrDefault();
            ViewObject.ObjectTree = Regions;
        }
    }
}