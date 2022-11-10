using INDELAPPEnd.DataViewModels;
using INDELAPPEnd.Helpers;
using INDELLAPPEnd.Models;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegionsPage : ContentPage, IExceptionHandler
    {
        public ObservableCollection<CustomRegionClass> ObjectTree = new ObservableCollection<CustomRegionClass>();
        public ObservableCollection<CustomRegionClass> Regions = new ObservableCollection<CustomRegionClass>();
        private ToolbarItem SettingsToolbarItem { get; set; }
        public RegionsPage(ObservableCollection<CustomRegionClass> regions)
        {
            InitializeComponent();
            ObjectTree = regions;
            Regions = regions;
            regionList.ItemsSource = regions;
            Title = "Все регионы";
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
            regionList.IsEnabled = true;
            base.OnAppearing();
        }

        private async void SelectedRegion(object sender, ItemTappedEventArgs e)
        {
            regionList.IsEnabled = false;
            var selectedRegion = e.Item as CustomRegionClass;
            await Application.Current.MainPage.Navigation.PushAsync(new LocationsPage(selectedRegion, Regions), true);
        }

        private async void SearchButtonClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage(ObjectTree), true);
        }

        private async void SettingsButtonClick(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(), true);
        }

        private async void GetRegions()
        {
            await Task.Delay(3000);
            Refresh.IsRefreshing = true;
            var objectTree = AppRepository.Region.View(Links.APIObjectTreeGet,
                new ObservableCollection<CustomRegionClass>(), true);
            Refresh.IsRefreshing = false;
            SetMessage(objectTree.Key, objectTree.Value);
        }

        private void Refresh_Refreshing(object sender, System.EventArgs e)
        {
            GetRegions();
        }

        public void SetMessage<T>(HttpStatusCode code, T data)
        {
            switch (code)
            {
                case HttpStatusCode.OK:
                    ObjectTree = data as ObservableCollection<CustomRegionClass>;
                    Regions = ObjectTree;
                    regionList.ItemsSource = ObjectTree;
                    break;
                case HttpStatusCode.InternalServerError:
                    if (Application.Current.MainPage.
                        DisplayAlert("Ошибка загрузки", "Не удалось загрузить регионы.", "Попробовать снова", "Отмена").Result)
                        Refresh_Refreshing(new object(), new System.EventArgs());
                    break;
            }
        }
    }
}