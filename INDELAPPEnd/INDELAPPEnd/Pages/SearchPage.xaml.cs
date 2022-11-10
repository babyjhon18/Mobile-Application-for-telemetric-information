using INDELAPPEnd.DataViewModels;
using INDELAPPEnd.Helpers;
using INDELAPPEnd.Pages.ObjectPages;
using INDELLAPPEnd.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage, IExceptionHandler
    {
        public ObservableCollection<CustomRegionClass> Regions = new ObservableCollection<CustomRegionClass>();
        public SearchPage(ObservableCollection<CustomRegionClass> regions)
        {
            InitializeComponent();
            Regions = regions;
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            noResultHint.IsVisible = false;
            loadingIndicator.IsEnabled = false;
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
            Suggestions.IsVisible = false;
            roundedSearchBar.Placeholder = "Поиск по имени";
            roundedSearchBar.Focus();
        }

        private async void Search_Clicked(object sender, EventArgs e)
        {
            noResultHint.IsVisible = false;
            Suggestions.IsVisible = false;
            loadingIndicator.IsEnabled = true;
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
            await Task.Delay(1000);
            switch (roundedSearchBar.Placeholder)
            {
                case "Поиск по имени":
                    SearchResult(SearchType.stObjectByName, roundedSearchBar.Text);
                    break;
                case "Поиск по номеру RTU":
                    SearchResult(SearchType.stObjectByRTU, roundedSearchBar.Text);
                    break;
                case "Поиск по номеру телефона":
                    SearchResult(SearchType.stObjectByTelephoneNumber, roundedSearchBar.Text);
                    break;
                case "Поиск по номеру прибора":
                    SearchResult(SearchType.stObjectByCounterSerialNumber, roundedSearchBar.Text);
                    break;
                case "Поиск по IP адресу":
                    SearchResult(SearchType.stObjectByIP, roundedSearchBar.Text);
                    break;
            }
        }

        private void ByName_Clicked(object sender, EventArgs e)
        {
            roundedSearchBar.Placeholder = "Поиск по имени";
            roundedSearchBar.Keyboard = Keyboard.Default;
            roundedSearchBar.Focus();
        }

        private void ByRTU_Clicked(object sender, EventArgs e)
        {
            roundedSearchBar.Placeholder = "Поиск по номеру RTU";
            roundedSearchBar.Keyboard = Keyboard.Numeric;
            roundedSearchBar.Focus();
        }

        private void ByPhoneNumber_Clicked(object sender, EventArgs e)
        {
            roundedSearchBar.Placeholder = "Поиск по номеру телефона";
            roundedSearchBar.Keyboard = Keyboard.Telephone;
            roundedSearchBar.Focus();
        }

        private void ByDeviceNumber_Clicked(object sender, EventArgs e)
        {
            roundedSearchBar.Placeholder = "Поиск по номеру прибора";
            roundedSearchBar.Keyboard = Keyboard.Numeric;
            roundedSearchBar.Focus();
        }

        private void ByIPAddress_Clicked(object sender, EventArgs e)
        {
            roundedSearchBar.Placeholder = "Поиск по IP адресу";
            roundedSearchBar.Keyboard = Keyboard.Numeric;
            roundedSearchBar.Focus();
        }

        public void SearchResult(SearchType searchType, string condition)
        {
            var suggestion = AppRepository.System.View(Links.APISystemSearch +
                "?searchType=" + Convert.ToInt32(searchType) + "&condition=" + condition, new List<BaseObjectClass>(), true);
            SetMessage(suggestion.Key, suggestion.Value);
            return;
        }

        private async void SelectedObject(object sender, ItemTappedEventArgs e)
        {
            BaseObjectClass baseObject = e.Item as BaseObjectClass;
            await Navigation.PushAsync(new ObjectTabbedPage(baseObject.ID, baseObject.Name, Regions), true);
        }

        public void SetMessage<T>(HttpStatusCode code, T data)
        {
            switch (code)
            {
                case HttpStatusCode.OK:
                    List<BaseObjectClass> Data = data as List<BaseObjectClass>;
                    if (data != null && data.ToString() != "[]" && Data.Count != 0)
                    {
                        Suggestions.ItemsSource = Data;
                        loadingIndicator.IsEnabled = false;
                        loadingIndicator.IsRunning = false;
                        loadingIndicator.IsVisible = false;
                        Suggestions.IsVisible = true;
                    }
                    else
                    {
                        loadingIndicator.IsEnabled = false;
                        loadingIndicator.IsRunning = false;
                        loadingIndicator.IsVisible = false;
                        Suggestions.IsVisible = false;
                        noResultHint.IsVisible = true;
                    }
                    break;
                case HttpStatusCode.InternalServerError:
                    loadingIndicator.IsEnabled = false;
                    loadingIndicator.IsRunning = false;
                    loadingIndicator.IsVisible = false;
                    Suggestions.IsVisible = false;
                    noResultHint.IsVisible = true;
                    break;
            }
        }
    }
}