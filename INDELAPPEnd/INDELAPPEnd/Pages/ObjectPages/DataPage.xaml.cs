using INDELAPPEnd.DataViewModels;
using INDELAPPEnd.Helpers;
using INDELAPPEnd.Models;
using INDELAPPEnd.ViewModels;
using INDELLAPPEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages.ObjectPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataPage : ContentPage
    {
        public ObjectViewClass ViewObject { get; set; }
        public DataPage()
        {
            InitializeComponent();
            ViewObject = new ObjectViewClass();
            SelectedArchiveType = new BaseItemClass() { ID = 0, Name = "Текущие" };
            archiveTypeEntry.Text = "Текущие";
            scrollViewData.IsVisible = false;
            loadingIndicator.IsVisible = false;
            loadingFrame.IsVisible = false;
            noDataHint.IsVisible = false;
            TapGestureRecognizer archiveTypeTapped = new TapGestureRecognizer();
            archiveTypeTapped.Tapped += ArchiveTypeEntry_Tapped;
            archiveTypeFrame.GestureRecognizers.Add(archiveTypeTapped);
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            noCountersHint.IsVisible = false;
            if (ViewObject.Object.Device.Counters.Count == 0)
            {
                noCountersHint.IsVisible = true;
                dataGrid.IsVisible = false;
            }
            else
            {
                SelectedCounter = ViewObject.Object.Device.Counters.FirstOrDefault();
                noCountersHint.IsVisible = false;
                dataGrid.IsVisible = true;
            }
            base.OnAppearing();
        }

        public void SetData(ObjectViewClass viewObject)
        {
            ViewObject = viewObject;
            DateTime dateToday = DateTime.Now;
            dateFrom.Date = new DateTime(dateToday.Year, dateToday.Month, 1);
            counterCollectionView.ItemsSource = ViewObject.Object.Device.Counters;
        }

        private CounterClass previousCounter;
        private CounterClass selectedCounter;
        public CounterClass SelectedCounter
        {
            get { return selectedCounter; }
            set
            {
                previousCounter = selectedCounter;
                if (selectedCounter != value && value != null)
                {
                    selectedCounter = value;
                    OnPropertyChanged("SelectedCounter");
                }
                if (value == null)
                {
                    SelectedCounter = previousCounter;
                    OnPropertyChanged("SelectedCounter");
                }
            }
        }

        private string firstDate;
        public string FirstDate
        {
            get { return firstDate; }
            set
            {
                firstDate = value;
                OnPropertyChanged("FirstDate");
            }
        }

        private string expCollapsed;
        public string ExpCollapsed
        {
            get { return expCollapsed; }
            set
            {
                expCollapsed = value;
                OnPropertyChanged("ExpCollapsed");
            }
        }

        private async void GetArchiveDataClicked(object sender, EventArgs e)
        {
            archiveDataStack.Children.Clear();
            noDataHint.IsVisible = false;
            loadingIndicator.IsVisible = true;
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsEnabled = true;
            loadingFrame.IsVisible = true;
            await Task.Delay(2000);
            List<CustomIndicatorViewModel> _Indicator = new List<CustomIndicatorViewModel>();
            CustomIndicatorViewModel indicatorView = new CustomIndicatorViewModel();
            string _dateFrom = dateFrom.Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            string _toDate = toDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            CounterListDataPoint counterList = AppRepository.Data.View(Links.APIGetArchiveData +
                  "?counterID=" + SelectedCounter.ID + "&archiveType=" + SelectedArchiveType.ID + "&dateFrom="
                        + _dateFrom + "&toDate=" + _toDate, new CounterListDataPoint(), true).Value;
            if (counterList.Data.Count == 0)
            {
                noDataHint.IsVisible = true;
                loadingIndicator.IsVisible = false;
                loadingFrame.IsVisible = false;
            }
            else
            {
                foreach (var listDataPoint in counterList.Data)
                {
                    foreach (var dataPoint in listDataPoint.Points)
                    {
                        foreach (var field in dataPoint.Fields)
                        {
                            indicatorView = new CustomIndicatorViewModel();
                            indicatorView.IndicatorName = field.Name;
                            _Indicator.Add(indicatorView);
                        }
                        break;
                    }
                    break;
                }
                foreach (var indic in _Indicator)
                {
                    foreach (var listDataPoint in counterList.Data)
                    {
                        foreach (var dataPoint in listDataPoint.Points)
                        {
                            foreach (var field in dataPoint.Fields.Where(f => f.Name == indic.IndicatorName))
                            {
                                indic.FieldsViews.Add(new FieldsView()
                                {
                                    Value = field.Value,
                                    Date = dataPoint.TimeStamp.ToString("dd.MM.yyyy HH:mm:ss")
                                });
                            }
                        }
                    }
                }
                Color backgroundColor = Color.FromHex("#f0f0f0");
                int i = 0;
                foreach (var indic in _Indicator)
                {
                    if (indic.FieldsViews.FirstOrDefault().Value != "")
                    {
                        if (i % 2 == 0)
                            backgroundColor = Color.FromHex("#f0f0f0");
                        else
                            backgroundColor = Color.White;
                        i++;
                        Grid headerGrid = new Grid()
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                        };

                        headerGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                        headerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                        headerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                        headerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 40 });

                        Label headerLabelName = new Label()
                        {
                            HorizontalTextAlignment = TextAlignment.Start,
                            VerticalTextAlignment = TextAlignment.Center,
                            Margin = new Thickness(10, 0, 0, 0),
                            Text = indic.IndicatorName,
                            FontAttributes = FontAttributes.Bold
                        };

                        Label headerLabelValue = new Label()
                        {
                            HorizontalTextAlignment = TextAlignment.Start,
                            VerticalTextAlignment = TextAlignment.Center,
                            Text = indic.FieldsViews[0].Value,
                        };

                        Image headerImage = new Image()
                        {
                            Source = "expand.png",
                            Margin = new Thickness(15, 15)
                        };

                        headerGrid.Children.Add(headerLabelName, 0, 0);
                        headerGrid.Children.Add(headerLabelValue, 1, 0);
                        headerGrid.Children.Add(headerImage, 2, 0);

                        Expander archiveDataExpander = new Expander()
                        {
                            Header = headerGrid,
                            BackgroundColor = backgroundColor,
                            ExpandAnimationEasing = Easing.CubicIn,
                            CollapseAnimationEasing = Easing.CubicOut
                        };

                        archiveDataExpander.Tapped += ArchiveDataExpander_Tapped;

                        Grid fields = new Grid();

                        StackLayout fieldsList = new StackLayout()
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            BackgroundColor = backgroundColor,
                            Spacing = 0,
                        };

                        FirstDate = indic.FieldsViews.FirstOrDefault().Date;
                        for (int f = 1; f < indic.FieldsViews.Count; f++)
                        {
                            fields = new Grid()
                            {
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                BackgroundColor = backgroundColor,
                            };

                            fields.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                            fields.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                            fields.ColumnDefinitions.Add(new ColumnDefinition() { Width = 40 });

                            fields.Children.Add(new Label()
                            {
                                HorizontalTextAlignment = TextAlignment.Start,
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                Margin = new Thickness(10, 0, 0, 5),
                                Text = indic.FieldsViews[f].Date,
                            }, 0, 0);
                            fields.Children.Add(new Label()
                            {
                                HorizontalTextAlignment = TextAlignment.Start,
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                Margin = new Thickness(0, 0, 10, 5),
                                Text = indic.FieldsViews[f].Value,
                            }, 1, 0);
                            fieldsList.Children.Add(fields);
                        }
                        archiveDataExpander.Content = fieldsList;
                        archiveDataStack.Children.Add(archiveDataExpander);
                    }
                }
                loadingFrame.IsVisible = false;
                loadingIndicator.IsVisible = false;
                scrollViewData.IsVisible = true;
            }
        }

        private void ArchiveDataExpander_Tapped(object sender, EventArgs e)
        {
            var expander = sender as Expander;
            var childList = expander.GetChildren();
            var stack = childList.Where(ch => ch is StackLayout).FirstOrDefault() as StackLayout;
            var grid = stack.Children.Where(ch => ch is Grid).FirstOrDefault() as Grid;
            var image = grid.Children.Where(ch => ch is Image).FirstOrDefault() as Image;
            if (image.Source.ToString() == "File: expand.png")
            {
                image.Source = "collapse.png";
            }
            else if (image.Source.ToString() == "File: collapse.png")
            {
                image.Source = "expand.png";
            }
        }

        private BaseItemClass SelectedArchiveType { get; set; }

        private async void ArchiveTypeEntry_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<ListViewPage, BaseItemClass>(this, "SetArchiveType", (page, archiveType) =>
            {
                SelectedArchiveType = archiveType;
                archiveTypeEntry.Text = SelectedArchiveType.Name;
                MessagingCenter.Unsubscribe<ListViewPage, BaseItemClass>(this, "SetArchiveType");
            });
            await Navigation.PushModalAsync(new ListViewPage(new List<BaseItemClass>(), 10, archiveTypeEntry.Text));
        }
    }
}