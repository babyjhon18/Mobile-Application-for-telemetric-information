using INDELAPPEnd.DataViewModels;
using INDELAPPEnd.Helpers;
using INDELAPPEnd.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages.ObjectPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataByCounterPage : ContentPage, IExceptionHandler
    {
        CounterListDataPoint DataPoint = new CounterListDataPoint();
        protected async override void OnAppearing()
        {
            noDataHint.IsVisible = false;
            dataGrid.IsVisible = false;
            loadingIndicator.IsVisible = true;
            loadingIndicator.IsEnabled = true;
            loadingIndicator.IsRunning = true;
            await Task.Delay(1000);
            GetDataByCounter(ObjectID, CounterID);
            if (DataPoint.Data.Count == 0)
            {
                loadingIndicator.IsVisible = false;
                loadingIndicator.IsEnabled = false;
                loadingIndicator.IsRunning = false;
                noDataHint.IsVisible = true;
            }
            else
            {
                foreach (var data in DataPoint.Data)
                {
                    int i = 0;
                    foreach (var field in data.Points[0].Fields)
                    {
                        if (i % 2 == 0)
                            field.Color = Color.FromHex("#f0f0f0");
                        else
                            field.Color = Color.White;
                        i++;
                    }
                }
                archiveTypeCollectionView.ItemsSource = DataPoint.Data;
                SelectedArchiveType = DataPoint.Data.FirstOrDefault();
                dataGrid.IsVisible = true;
                loadingIndicator.IsVisible = false;
                loadingIndicator.IsEnabled = false;
                loadingIndicator.IsRunning = false;
            }
        }

        private string NameOfCounter { get; set; }
        private int ObjectID { get; set; }
        private int CounterID { get; set; }

        public DataByCounterPage(string nameOfCounter, int objectID, int counterID)
        {
            InitializeComponent();
            NameOfCounter = nameOfCounter;
            Title = NameOfCounter;
            ObjectID = objectID;
            CounterID = counterID;
            BindingContext = this;
        }

        private ListDataPoint previousListDataPoint;
        private ListDataPoint selectedArchiveType;
        public ListDataPoint SelectedArchiveType
        {
            get { return selectedArchiveType; }
            set
            {
                previousListDataPoint = selectedArchiveType;
                if (selectedArchiveType != value && value != null)
                {
                    selectedArchiveType = value;
                    dataFrame.BindingContext = selectedArchiveType.Points[0];
                    OnPropertyChanged("SelectedArchiveType");
                }
                if (value == null)
                {
                    SelectedArchiveType = previousListDataPoint;
                    OnPropertyChanged("SelectedArchiveType");
                }
            }
        }

        private void GetDataByCounter(int objectId, int counterId)
        {
            var result = AppRepository.Data.View(Links.APIGetLastData + "?objectID="
                + objectId + "&counterID=" + counterId, new LastDataByObject(), true);
            SetMessage(result.Key, result.Value);

        }

        public void SetMessage<T>(HttpStatusCode code, T data)
        {
            LastDataByObject Data = data as LastDataByObject;
            if (Data == null)
            {
                loadingIndicator.IsVisible = false;
                loadingIndicator.IsEnabled = false;
                loadingIndicator.IsRunning = false;
                noDataHint.IsVisible = true;
            }
            else
            {
                DataPoint.Data = Data.ListOfCounters.FirstOrDefault().Data;
            }
        }
    }
}