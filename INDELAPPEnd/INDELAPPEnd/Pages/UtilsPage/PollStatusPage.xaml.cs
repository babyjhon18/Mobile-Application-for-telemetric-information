using INDELAPPEnd.DataViewModels;
using INDELAPPEnd.Helpers;
using INDELLAPPEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages.UtilsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PollStatusPage : ContentPage
    {
        public PollStatusPage()
        {
            InitializeComponent();
        }

        private Color statusTextColor;
        public Color StatusTextColor
        {
            get { return statusTextColor; }
            set
            {
                statusTextColor = value;
                OnPropertyChanged("StatusTextColor");
            }
        }

        private string imageSource;
        public string ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }
        private string _EntityName { get; set; }
        public PollStatusPage(ScheduleClass schedule, string EntityName)
        {
            InitializeComponent();
            _EntityName = EntityName;
            backButton.IsVisible = false;
            StartPolling(schedule.ID);
            BindingContext = this;
        }

        private async void StartPolling(string ID)
        {
            ObjectScheduleClass objectSchedule = new ObjectScheduleClass();
            bool done = false;
            objectSchedule = AppRepository.Schedule.View(Links.APIScheduleCheckStatus + "?scheduleID=" + ID,
                new List<ObjectScheduleClass>(), true).Value.FirstOrDefault();
            if (objectSchedule == null)
            {
                ImageSource = "NoPoll.png";
                StatusTextColor = Color.FromHex("#337ab7");
                Status = "Объект уже находится в очереди опроса. Повторите попытку позже.";
                return;
            };
            ImageSource = "pollObjectIcon.gif";
            StatusTextColor = Color.FromHex("#337ab7");
            Status = _EntityName + " - " + objectSchedule.Description;
            while (!done)
            {
                switch (objectSchedule.Description)
                {
                    case "ожидание":
                        objectSchedule = AppRepository.Schedule.View(Links.APIScheduleCheckStatus + "?scheduleID=" + ID,
                            new List<ObjectScheduleClass>(), true).Value.FirstOrDefault();
                        if (objectSchedule == null)
                            Status = _EntityName + " - " + objectSchedule.Description;
                        StatusTextColor = Color.FromHex("#337ab7");
                        break;
                    case "выполнение":
                        objectSchedule = AppRepository.Schedule.View(Links.APIScheduleCheckStatus + "?scheduleID=" + ID,
                            new List<ObjectScheduleClass>(), true).Value.FirstOrDefault();
                        if (objectSchedule == null)
                            Status = _EntityName + " - " + objectSchedule.Description;
                        StatusTextColor = Color.FromHex("#337ab7");
                        break;
                    case "успешно выполнено":
                        ImageSource = "ok.png";
                        Status = _EntityName + " - " + objectSchedule.Description;
                        StatusTextColor = Color.Green;
                        done = true;
                        backButton.IsVisible = true;
                        MessagingCenter.Send(this, "UpdatePollObject", new Object());
                        break;
                    case "ошибка связи":
                        ImageSource = "ConnectionError.png";
                        Status = _EntityName + " - " + objectSchedule.Description;
                        StatusTextColor = Color.Maroon;
                        MessagingCenter.Send(this, "UpdatePollObject", new Object());
                        backButton.IsVisible = true;
                        done = true;
                        break;
                    case "нет ответа":
                        ImageSource = "DataErrorCounter.png";
                        Status = _EntityName + " - " + objectSchedule.Description;
                        StatusTextColor = Color.Maroon;
                        MessagingCenter.Send(this, "UpdatePollObject", new Object());
                        done = true;
                        break;
                    case "номер занят":
                        ImageSource = "DataErrorCounter.png";
                        Status = _EntityName + " - " + objectSchedule.Description;
                        StatusTextColor = Color.Maroon;
                        MessagingCenter.Send(this, "UpdatePollObject", new Object());
                        backButton.IsVisible = true;
                        done = true;
                        break;
                    case "таймаут":
                        ImageSource = "TimeOut.png";
                        Status = _EntityName + " - " + objectSchedule.Description;
                        StatusTextColor = Color.Purple;
                        MessagingCenter.Send(this, "UpdatePollObject", new Object());
                        backButton.IsVisible = true;
                        done = true;
                        break;
                    case "ошибка данных":
                        ImageSource = "DataError.png";
                        Status = _EntityName + " - " + objectSchedule.Description;
                        StatusTextColor = Color.Red;
                        MessagingCenter.Send(this, "UpdatePollObject", new Object());
                        backButton.IsVisible = true;
                        done = true;
                        break;
                    default:
                        ImageSource = "DataError.png";
                        Status = _EntityName + " - " + "не известно";
                        StatusTextColor = Color.Red;
                        MessagingCenter.Send(this, "UpdatePollObject", new Object());
                        backButton.IsVisible = true;
                        done = true;
                        break;
                }
                await Task.Delay(1000);
            }
        }

        private async void BackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }
    }
}