using INDELAPPEnd.DataViewModels;
using INDELAPPEnd.Helpers;
using INDELAPPEnd.Pages.UtilsPage;
using INDELLAPPEnd.Models;
using System;
using System.Linq;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages.ObjectPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PollPage : ContentPage, IExceptionHandler
    {
        private CommonObjectClass CurrentObject = new CommonObjectClass();
        private CounterClass CurrentCounter = new CounterClass();
        private RadioButton CurrentTypeRadioButton = new RadioButton();
        private RadioButton CurrentEntityRadioButton = new RadioButton();
        public ObjectViewClass ViewObject { get; set; }
        private int index { get; set; }
        public PollPage()
        {
            InitializeComponent();
            ViewObject = new ObjectViewClass();
            if (!Settings.IsAdmin)
            {
                if (!Settings.claims.Contains(new System.Collections.Generic.KeyValuePair<string, string>("Schedule", "ReadCurrent")))
                    Current.IsVisible = false;
                if (!Settings.claims.Contains(new System.Collections.Generic.KeyValuePair<string, string>("Schedule", "ReadArchive")))
                    Archive.IsVisible = false;
            }
            Current.IsChecked = true;
            CurrentTypeRadioButton.Value = "Текущие";
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            dayEntry.Keyboard = Keyboard.Numeric;
            hoursEntry.Keyboard = Keyboard.Numeric;
            minutesEntry.Keyboard = Keyboard.Numeric;
            if (ViewObject.Object.Device.Counters.Count == 0)
            {
                noCountersHint.IsVisible = true;
                scrollViewPoll.IsVisible = false;
            }
            else
            {
                noCountersHint.IsVisible = false;
                scrollViewPoll.IsVisible = true;
            }
            base.OnAppearing();
        }

        private void ArchiveType_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var check = sender as RadioButton;
            if (check != CurrentTypeRadioButton)
            {
                if (check.Value.ToString() == "Архивные")
                {
                    archiveTypeFields.IsVisible = true;
                    if (dayCheck.IsChecked || hoursCheck.IsChecked || minutesCheck.IsChecked)
                        pollButton.IsEnabled = true;
                    else
                        pollButton.IsEnabled = false;
                    dayEntry.Focus();
                }
                else if (check.Value.ToString() == "Текущие")
                {
                    archiveTypeFields.IsVisible = false;
                    pollButton.IsEnabled = true;
                }
            }
            CurrentTypeRadioButton = check;
        }

        private void ObjectPollSelection_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var check = sender as RadioButton;
            if (check != CurrentEntityRadioButton)
            {
                if (check.BindingContext is CommonObjectClass)
                {
                    CurrentObject = ViewObject.Object;
                }
                else
                {
                    var counter = check.BindingContext as CounterClass;
                    CurrentCounter = counter;
                }
            }
            CurrentEntityRadioButton = check;
        }

        public void SetData(ObjectViewClass viewObject)
        {
            index = 0;
            ViewObject = viewObject;
            CurrentObject = ViewObject.Object;
            CurrentEntityRadioButton.BindingContext = ViewObject.Object;
            objectPollStackLayout.Children.Clear();
            if (CurrentTypeRadioButton.Value.ToString() == "Архивные")
                archiveTypeFields.IsVisible = true;
            RadioButton fullObjectRadioButton = new RadioButton()
            {
                Content = "Весь объект",
                Value = "Весь объект",
                BindingContext = ViewObject.Object,
                FontAttributes = FontAttributes.Bold,
                FontSize = 16,
                IsChecked = true
            };
            fullObjectRadioButton.CheckedChanged += ObjectPollSelection_CheckedChanged;
            objectPollStackLayout.Children.Insert(index, fullObjectRadioButton);
            foreach (var counter in ViewObject.Object.Device.Counters)
            {
                index += 1;
                RadioButton counterRadioButton = new RadioButton()
                {
                    Content = counter.Name,
                    Value = counter.Name,
                    BindingContext = counter,
                    FontAttributes = FontAttributes.None,
                    FontSize = 14
                };
                counterRadioButton.CheckedChanged += ObjectPollSelection_CheckedChanged;
                objectPollStackLayout.Children.Insert(index, counterRadioButton);
            }
            objectPollStackLayout.BindingContext = ViewObject.Object;
        }

        private void AcceptButton_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<PollStatusPage, Object>(this, "UpdatePollObject", (_page, obj) =>
            {
                var ObjectTabbedPage = _PollPage.Parent as ObjectTabbedPage;
                ObjectTabbedPage.GetObjectData();
                MessagingCenter.Unsubscribe<PollStatusPage, Object>(this, "UpdatePollObject");
            });
            ScheduleClass schedule = new ScheduleClass();
            if (CurrentTypeRadioButton.Value.ToString() == "Текущие")
            {
                if (CurrentEntityRadioButton.BindingContext is CommonObjectClass)
                {
                    object _Object = new
                    {
                        ScheduleParam = new
                        {
                            Type = 0,
                        },
                        ViewParam = new
                        {
                            ObjectID = ViewObject.Object.ID,
                        }
                    };
                    schedule = AppRepository.Schedule.Create<ScheduleClass>(Links.APIScheduleCurrent,
                            _Object, true).Value;
                    Navigation.PushModalAsync(new PollStatusPage(schedule, CurrentObject.Name), true);
                }
                else if (CurrentEntityRadioButton.BindingContext is CounterClass)
                {
                    var counter = CurrentEntityRadioButton.BindingContext as CounterClass;
                    object _Counter = new
                    {
                        ScheduleParam = new
                        {
                            Type = 0,
                        },
                        ViewParam = new
                        {
                            Counters = counter.ID.ToString(),
                        }
                    };
                    schedule = AppRepository.Schedule.Create<ScheduleClass>(Links.APIScheduleCurrent,
                            _Counter, true).Value;
                    Navigation.PushModalAsync(new PollStatusPage(schedule, CurrentObject.Name + " " + CurrentCounter.Name), true);
                }
            }
            else if (CurrentTypeRadioButton.Value.ToString() == "Архивные")
            {
                if (CurrentEntityRadioButton.BindingContext is CommonObjectClass)
                {
                    object _Object = new
                    {
                        ScheduleParam = new
                        {
                            DayCount = dayCheck.IsChecked ? dayEntry.Text.ToString() : "-1",
                            HourCount = hoursCheck.IsChecked ? hoursEntry.Text.ToString() : "-1",
                            MinuteCount = minutesCheck.IsChecked ? minutesEntry.Text.ToString() : "-1"
                        },
                        ViewParam = new
                        {
                            ObjectID = ViewObject.Object.ID,
                        }
                    };
                    schedule = AppRepository.Schedule.Create<ScheduleClass>(Links.APIScheduleArchive,
                            _Object, true).Value;
                    Navigation.PushModalAsync(new PollStatusPage(schedule, CurrentObject.Name), true);
                }
                else if (CurrentEntityRadioButton.BindingContext is CounterClass)
                {
                    var counter = CurrentEntityRadioButton.BindingContext as CounterClass;
                    object _Counter = new
                    {
                        ScheduleParam = new
                        {
                            DayCount = dayCheck.IsChecked ? dayEntry.Text.ToString() : "-1",
                            HourCount = hoursCheck.IsChecked ? hoursEntry.Text.ToString() : "-1",
                            MinuteCount = minutesCheck.IsChecked ? minutesEntry.Text.ToString() : "-1"
                        },
                        ViewParam = new
                        {
                            Counters = counter.ID.ToString(),
                        }
                    };
                    schedule = AppRepository.Schedule.Create<ScheduleClass>(Links.APIScheduleArchive,
                            _Counter, true).Value;
                    Navigation.PushModalAsync(new PollStatusPage(schedule, CurrentObject.Name + " " + CurrentCounter.Name), true);
                }
            }
        }

        public void SetMessage<T>(HttpStatusCode code, T data)
        {
            throw new NotImplementedException();
        }

        private void Check_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ArchiveFieldsChanging();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var anyEntry = sender as Entry;
            if (IsOnlyDigits(anyEntry.Text))
                ArchiveFieldsChanging();
            else
            {
                pollButton.IsEnabled = false;
                anyEntry.Text = anyEntry.Text.TrimEnd(anyEntry.Text[anyEntry.Text.Length - 1]);
            }
        }

        private bool IsOnlyDigits(string s)
        {
            return s.All(char.IsDigit);
        }

        private void ArchiveFieldsChanging()
        {
            pollButton.IsEnabled = false;
            if (dayCheck.IsChecked && !minutesCheck.IsChecked && !hoursCheck.IsChecked)
                if (!string.IsNullOrEmpty(dayEntry.Text))
                    if (dayEntry.Text.All(char.IsDigit))
                        pollButton.IsEnabled = true;

            if (!dayCheck.IsChecked && minutesCheck.IsChecked && !hoursCheck.IsChecked)
                if (!string.IsNullOrEmpty(minutesEntry.Text))
                    pollButton.IsEnabled = true;

            if (!dayCheck.IsChecked && !minutesCheck.IsChecked && hoursCheck.IsChecked)
                if (!string.IsNullOrEmpty(hoursEntry.Text))
                    pollButton.IsEnabled = true;

            if (dayCheck.IsChecked && minutesCheck.IsChecked && !hoursCheck.IsChecked)
                if (!string.IsNullOrEmpty(dayEntry.Text) && !string.IsNullOrEmpty(minutesEntry.Text))
                    pollButton.IsEnabled = true;

            if (dayCheck.IsChecked && !minutesCheck.IsChecked && hoursCheck.IsChecked)
                if (!string.IsNullOrEmpty(dayEntry.Text) && !string.IsNullOrEmpty(hoursEntry.Text))
                    pollButton.IsEnabled = true;

            if (!dayCheck.IsChecked && minutesCheck.IsChecked && hoursCheck.IsChecked)
                if (!string.IsNullOrEmpty(hoursEntry.Text) && !string.IsNullOrEmpty(minutesEntry.Text))
                    pollButton.IsEnabled = true;

            if (dayCheck.IsChecked && minutesCheck.IsChecked && hoursCheck.IsChecked)
                if (!string.IsNullOrEmpty(dayEntry.Text) && !string.IsNullOrEmpty(hoursEntry.Text)
                    && !string.IsNullOrEmpty(minutesEntry.Text))
                    pollButton.IsEnabled = true;

        }
    }
}