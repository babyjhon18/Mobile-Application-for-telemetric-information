using INDELAPPEnd.DataViewModels;
using INDELAPPEnd.Helpers;
using INDELAPPEnd.Models;
using INDELAPPEnd.Pages.UtilsPage;
using INDELLAPPEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages.ConfigurationPageToolbarPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCounterPage : ContentPage
    {
        private int PageType { get; set; }
        private string titleOfPage;
        public string TitleOfPage
        {
            get { return titleOfPage; }
            set
            {
                titleOfPage = value;
                OnPropertyChanged("TitleOfPage");
            }
        }
        //список скорости, контроллера
        private List<string> SpeedList = new List<string>() { "110", "150", "300", "600", "1200", "2400",
            "4800", "9600", "14400", "19200", "38400", "576000", "115200"};
        private CounterViewClass CounterViewClass { get; set; }
        private BaseItemClass SelectedPUType { get; set; }
        private BaseItemClass SelectedAccountingType { get; set; }
        private BaseItemClass SelectedEnvironment { get; set; }
        private EntityClass SelectedCounterState { get; set; }
        private string SelectedSpeed { get; set; }
        private EntityClass SelectedCounterType { get; set; }
        public EditCounterPage()
        {
            InitializeComponent();
        }

        //метод, который предназначен для обработки кнопки назад
        protected override bool OnBackButtonPressed()
        {
            if (PageType == 1)
            {
                if (I != 0)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        MessagingCenter.Subscribe<AcceptDeclinePage, bool>(this, "AcceptOrDecline", async (page, boolItem) =>
                        {
                            if (page.Result)
                                AcceptButton_Click(new object(), new EventArgs());
                            else
                                await Navigation.PopModalAsync(true);
                            MessagingCenter.Unsubscribe<AcceptDeclinePage, bool>(this, "AcceptOrDecline");
                        });
                        await Navigation.PushModalAsync(new AcceptDeclinePage("Сохранить изменения?", "Ок", "Отмена", true));
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopModalAsync(true);
                    });
                }
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopModalAsync(true);
                });
            }
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        // 0 - new, 1 - edit
        public EditCounterPage(CounterViewClass viewCounter, int pageType)
        {
            CounterViewClass = viewCounter;
            InitializeComponent();
            I = 0;
            CounterViewClass.AccountingTypes.Insert(0, new BaseItemClass() { Name = "Не выбрано" });
            CounterViewClass.EnvironmentTypes.Insert(0, new BaseItemClass() { Name = "Не выбрано" });
            CounterViewClass.States.Insert(0, new BaseItemClass() { Name = "Не выбрано" });
            loadingIndicator.IsVisible = false;
            loadingIndicator.IsEnabled = false;
            loadingIndicator.IsRunning = false;
            counterInfo.IsVisible = true;
            counterConnection.IsVisible = true;
            acceptButton.IsVisible = true;
            //если хотим добавить счетчик, то заполняем поля значениями по умолчанию
            if (pageType == 0)
            {
                PageType = 0;
                TitleOfPage = "Добавить счетчик";
                SelectedPUType = CounterViewClass.CounterTypes.FirstOrDefault();
                PUTypeEntry.Text = SelectedPUType.Name;
                SelectedAccountingType = CounterViewClass.AccountingTypes.FirstOrDefault();
                accountingTypeEntry.Text = SelectedAccountingType.Name;
                SelectedEnvironment = CounterViewClass.EnvironmentTypes.FirstOrDefault();
                environmentEntry.Text = SelectedAccountingType.Name;
                descriptionEntry.Text = SelectedPUType.Name;
                SelectedCounterState = CounterViewClass.States.FirstOrDefault();
                counterStateEntry.Text = SelectedCounterState.Name;
                counterSerialNumberEntry.Text = "";
                counterRegistrationNumberEntry.Text = "";
                deviceNumberEntry.Text = "1";
                deviceModuleEntry.Text = "1";
                isActiveSwitch.IsToggled = CounterViewClass.Counter.Active;
                IPPortEntry.Text = "0";
                SelectedSpeed = SpeedList.FirstOrDefault().ToString();
                speedEntry.Text = SelectedSpeed;
                addressEntry.Text = "0";
                passwordEntry.Text = "";
                chanelNumberEntry.Text = "1";
                SelectedCounterType = CounterViewClass.DeviceTypes.FirstOrDefault();
                counterTypeEntry.Text = SelectedCounterType.Name;
            }
            //если происходит изменение, то заполняем поля полученным ранее счетчиком
            if (pageType == 1)
            {
                PageType = pageType;
                TitleOfPage = "Редактировать счетчик";
                SelectedPUType = CounterViewClass.CounterTypes.Where(spu =>
                    spu.ID == CounterViewClass.Counter.Type.ID).FirstOrDefault();
                SelectedPUType = SelectedPUType == null ?
                    CounterViewClass.CounterTypes.FirstOrDefault() : SelectedPUType;
                PUTypeEntry.Text = SelectedPUType.Name;
                SelectedAccountingType = CounterViewClass.AccountingTypes.Where(sat =>
                    sat.ID == CounterViewClass.Counter.Accounting.Type.ID).FirstOrDefault();
                SelectedAccountingType = SelectedAccountingType == null ?
                    CounterViewClass.AccountingTypes.FirstOrDefault() : SelectedAccountingType;
                accountingTypeEntry.Text = SelectedAccountingType.Name;
                SelectedEnvironment = CounterViewClass.EnvironmentTypes.Where(se =>
                    se.Name == CounterViewClass.Counter.Environment.Type.Name).FirstOrDefault();
                SelectedEnvironment = SelectedEnvironment == null ?
                    CounterViewClass.EnvironmentTypes.FirstOrDefault() : SelectedEnvironment;
                environmentEntry.Text = SelectedEnvironment.Name;
                descriptionEntry.Text = CounterViewClass.Counter.Name;
                SelectedCounterState = CounterViewClass.States.Where(scs =>
                    scs.ID == CounterViewClass.Counter.State.ID).FirstOrDefault();
                SelectedCounterState = SelectedCounterState == null ?
                    CounterViewClass.States.FirstOrDefault() : SelectedCounterState;
                counterStateEntry.Text = SelectedCounterState.Name;
                counterSerialNumberEntry.Text = CounterViewClass.Counter.SerialNumber;
                counterRegistrationNumberEntry.Text = CounterViewClass.Counter.RegNumber;
                deviceNumberEntry.Text = CounterViewClass.Counter.SlotNumber.ToString();
                deviceModuleEntry.Text = CounterViewClass.Counter.ModuleNumber.ToString();
                isActiveSwitch.IsToggled = CounterViewClass.Counter.Active;
                IPPortEntry.Text = CounterViewClass.Counter.PhysicalConnection.Port.ToString();
                SelectedSpeed = CounterViewClass.Counter.PhysicalConnection.Speed.ToString();
                speedEntry.Text = SelectedSpeed;
                addressEntry.Text = CounterViewClass.Counter.PhysicalConnection.Address.ToString();
                passwordEntry.Text = CounterViewClass.Counter.PhysicalConnection.Password;
                chanelNumberEntry.Text = CounterViewClass.Counter.PhysicalConnection.Channel.ToString();
                SelectedCounterType = CounterViewClass.DeviceTypes.Where(dt =>
                    dt.ID == CounterViewClass.Counter.PhysicalConnection.Device.Type.ID).FirstOrDefault();
                SelectedCounterType = SelectedCounterType == null ?
                    CounterViewClass.DeviceTypes.FirstOrDefault() : SelectedCounterType;
                counterTypeEntry.Text = SelectedCounterType.Name;
                //создание дополнительных кнопок для взаимодействия с прибором учета
                ToolbarItem cloneToolBarItem = new ToolbarItem()
                {
                    Order = ToolbarItemOrder.Secondary,
                    Text = "Клонировать прибор",
                };
                if (Settings.claims.Contains(new KeyValuePair<string, string>("Counter", "Create")) || Settings.IsAdmin)
                    ToolbarItems.Add(cloneToolBarItem);
                cloneToolBarItem.Clicked += CloneCounter_Clicked;
                ToolbarItem deleteToolBarItem = new ToolbarItem()
                {
                    Order = ToolbarItemOrder.Secondary,
                    Text = "Удалить прибор",
                };
                deleteToolBarItem.Clicked += DeleteCounter_Clicked;
                if (Settings.claims.Contains(new KeyValuePair<string, string>("Counter", "Delete")) || Settings.IsAdmin)
                    ToolbarItems.Add(deleteToolBarItem);
            }
            TapGestureRecognizer puTypeTap = new TapGestureRecognizer();
            puTypeTap.Tapped += PuTypeEntry_Tapped;
            PUTypeFrame.GestureRecognizers.Add(puTypeTap);
            TapGestureRecognizer accountingTypeTap = new TapGestureRecognizer();
            accountingTypeTap.Tapped += AccountingTypeEntry_Tapped;
            accountingTypeFrame.GestureRecognizers.Add(accountingTypeTap);
            TapGestureRecognizer environmentTap = new TapGestureRecognizer();
            environmentTap.Tapped += EnvironmentEntry_Tapped;
            environmentFrame.GestureRecognizers.Add(environmentTap);
            TapGestureRecognizer counterStateTap = new TapGestureRecognizer();
            counterStateTap.Tapped += CounterStateEntry_Tapped;
            counterStateFrame.GestureRecognizers.Add(counterStateTap);
            TapGestureRecognizer speedTap = new TapGestureRecognizer();
            speedTap.Tapped += SpeedEntry_Tapped;
            speedFrame.GestureRecognizers.Add(speedTap);
            TapGestureRecognizer counterTypeTap = new TapGestureRecognizer();
            counterTypeTap.Tapped += CounterTypeEntry_Tapped;
            counterTypeFrame.GestureRecognizers.Add(counterTypeTap);
            BindingContext = this;
        }

        private async void PuTypeEntry_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<ListViewPage, BaseItemClass>(this, "SetPuType", (page, puType) =>
            {
                I++;
                SelectedPUType = puType;
                if (puType.Name == "Не выбрано")
                {
                    PUTypeEntry.Text = "Не выбрано";
                    descriptionEntry.Text = "Нет описания";
                }
                else
                {
                    PUTypeEntry.Text = puType.Name;
                    descriptionEntry.Text = puType.Name;
                }
                MessagingCenter.Unsubscribe<ListViewPage, BaseItemClass>(this, "SetPuType");
            });
            await Navigation.PushModalAsync(new ListViewPage(CounterViewClass.CounterTypes, 4,
                PUTypeEntry.Text == "" || PUTypeEntry == null ? "ART-05 (uni)" : PUTypeEntry.Text));
        }

        private async void AccountingTypeEntry_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<ListViewPage, BaseItemClass>(this, "SetAccounting", (page, accountingType) =>
            {
                I++;
                SelectedAccountingType = accountingType;
                if (accountingType.Name == "Не выбрано")
                    accountingTypeEntry.Text = "Не выбрано";
                else
                    accountingTypeEntry.Text = accountingType.Name;
                MessagingCenter.Unsubscribe<ListViewPage, BaseItemClass>(this, "SetAccounting");
            });
            await Navigation.PushModalAsync(new ListViewPage(CounterViewClass.AccountingTypes, 5,
                accountingTypeEntry.Text == "" || accountingTypeEntry == null ? "Не выбрано" : accountingTypeEntry.Text));
        }

        private async void EnvironmentEntry_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<ListViewPage, BaseItemClass>(this, "SetEnvironment", (page, environment) =>
            {
                I++;
                SelectedEnvironment = environment;
                if (environment.Name == "Не выбрано")
                    environmentEntry.Text = "Не выбрано";
                else
                    environmentEntry.Text = environment.Name;
                MessagingCenter.Unsubscribe<ListViewPage, BaseItemClass>(this, "SetEnvironment");
            });
            await Navigation.PushModalAsync(new ListViewPage(CounterViewClass.EnvironmentTypes, 6,
                environmentEntry.Text == "" || environmentEntry == null ? "Не выбрано" : environmentEntry.Text));
        }

        private async void CounterStateEntry_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<ListViewPage, BaseItemClass>(this, "SetCounterState", (page, counterState) =>
            {
                I++;
                SelectedCounterState = counterState;
                if (counterState.Name == "Не выбрано")
                    counterStateEntry.Text = "Не выбрано";
                else
                    counterStateEntry.Text = counterState.Name;
                MessagingCenter.Unsubscribe<ListViewPage, BaseItemClass>(this, "SetCounterState");
            });
            await Navigation.PushModalAsync(new ListViewPage(CounterViewClass.States, 7,
                counterStateEntry.Text == "" || counterStateEntry == null ? "Не выбрано" : counterStateEntry.Text));
        }

        private async void SpeedEntry_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<ListViewPage, string>(this, "SetSpeed", (page, speed) =>
            {
                I++;
                SelectedSpeed = speed;
                if (speed == "110")
                    speedEntry.Text = "110";
                else
                    speedEntry.Text = speed.ToString();
                MessagingCenter.Unsubscribe<ListViewPage, string>(this, "SetSpeed");
            });
            await Navigation.PushModalAsync(new ListViewPage(SpeedList, 8,
                speedEntry.Text == "" || speedEntry == null ? "110" : speedEntry.Text));
        }

        private async void CounterTypeEntry_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<ListViewPage, BaseItemClass>(this, "SetCounterType", (page, counterType) =>
            {
                I++;
                SelectedCounterType = counterType;
                if (counterType.Name == "Без контроллера")
                    counterTypeEntry.Text = "Без контроллера";
                else
                    counterTypeEntry.Text = counterType.Name;
                MessagingCenter.Unsubscribe<ListViewPage, BaseItemClass>(this, "SetCounterType");
            });
            await Navigation.PushModalAsync(new ListViewPage(CounterViewClass.DeviceTypes, 9,
                counterTypeEntry.Text == "" || counterTypeEntry == null ? "Без контроллера" : counterTypeEntry.Text));
        }

        private async void DeleteCounter_Clicked(object sender, System.EventArgs e)
        {
            MessagingCenter.Subscribe<AcceptDeclinePage, bool>(this, "AcceptOrDecline", async (page, boolItem) =>
            {
                if (page.Result)
                {
                    loadingIndicator.IsVisible = true;
                    loadingIndicator.IsEnabled = true;
                    loadingIndicator.IsRunning = true;
                    counterInfo.IsVisible = false;
                    counterConnection.IsVisible = false;
                    acceptButton.IsVisible = false;
                    await Task.Delay(1000);
                    var result = AppRepository.Counter.Delete<Object>(Links.APICounterDelete
                        + "?counterID=" + CounterViewClass.Counter.ID, true);
                    if (result.Key == System.Net.HttpStatusCode.OK)
                    {
                        MessagingCenter.Send(this, "UpdateObject", new Object());
                        await Navigation.PopModalAsync();
                    }
                }
                MessagingCenter.Unsubscribe<AcceptDeclinePage, bool>(this, "AcceptOrDecline");
            });
            await Navigation.PushModalAsync(new AcceptDeclinePage("Подтвердить удаление?", "Ок", "Отмена", true));
        }

        private async void CloneCounter_Clicked(object sender, System.EventArgs e)
        {
            MessagingCenter.Subscribe<CreateTreeItemPage, Object>(this, "CloneCounter", (page, obj) =>
            {
                MessagingCenter.Send(this, "UpdateObject");
                MessagingCenter.Unsubscribe<CreateTreeItemPage, Object>(this, "CloneCounter");
            });
            await Navigation.PushModalAsync(new CreateTreeItemPage(CounterViewClass.Counter.ID, 0));
        }

        private async void AcceptButton_Click(object sender, EventArgs e)
        {
            SelectedCounterState.ID = SelectedCounterState.ID != 0 ? SelectedCounterState.ID : -1;
            SelectedEnvironment.ID = SelectedEnvironment.ID != 0 ? SelectedEnvironment.ID : -1;
            SelectedAccountingType.ID = SelectedAccountingType.ID != 0 ? SelectedAccountingType.ID : -1;
            SelectedPUType.ID = SelectedPUType.ID != 0 ? SelectedPUType.ID : -1;
            if (descriptionEntry.Text == null || descriptionEntry.Text == "")
            {
                await Navigation.PushModalAsync(new AcceptDeclinePage("Ошибка заполнения. Поле описания не может быть пустым.",
                    "Ок", "", false));
                return;
            }
            if (PageType == 0)
            {
                var Counter = new
                {
                    name = descriptionEntry.Text,
                    slotNumber = Convert.ToInt32(deviceNumberEntry.Text),
                    moduleNumber = Convert.ToInt32(deviceModuleEntry.Text),
                    active = isActiveSwitch.IsToggled,
                    serialNumber = counterSerialNumberEntry.Text,
                    regNumber = counterRegistrationNumberEntry.Text,
                    device = new
                    {
                        id = CounterViewClass.Counter.DeviceID,
                    },
                    accounting = new
                    {
                        type = new
                        {
                            id = SelectedAccountingType.ID,
                        }
                    },
                    type = new
                    {
                        id = SelectedPUType.ID,
                    },
                    environment = new
                    {
                        type = new
                        {
                            id = SelectedEnvironment.ID,
                        }
                    },
                    state = new
                    {
                        id = SelectedCounterState.ID,
                        name = SelectedCounterState.Name
                    },
                    physicalConnection = new
                    {
                        port = IPPortEntry.Text,
                        speed = Convert.ToInt32(speedEntry.Text),
                        address = addressEntry.Text != null && addressEntry.Text != "" ? Convert.ToInt32(addressEntry.Text) : 0,
                        password = passwordEntry.Text,
                        device = new
                        {
                            type = new
                            {
                                id = SelectedCounterType.ID,
                            }
                        },
                        chanel = Convert.ToInt32(chanelNumberEntry.Text),
                    }
                };
                var result = AppRepository.Counter.Create<CounterClass>(Links.APICounterCreate, Counter, true);
                loadingIndicator.IsVisible = true;
                loadingIndicator.IsEnabled = true;
                loadingIndicator.IsRunning = true;
                counterInfo.IsVisible = false;
                counterConnection.IsVisible = false;
                acceptButton.IsVisible = false;
                await Task.Delay(1000);
                if (result.Value == null)
                {
                    await Navigation.PopModalAsync();
                    MessagingCenter.Send(this, "UpdateObject", new Object());
                }
            }
            if (PageType == 1)
            {
                var Counter = new
                {
                    id = CounterViewClass.Counter.ID,
                    name = descriptionEntry.Text,
                    slotNumber = Convert.ToInt32(deviceNumberEntry.Text),
                    moduleNumber = Convert.ToInt32(deviceModuleEntry.Text),
                    active = isActiveSwitch.IsToggled,
                    serialNumber = counterSerialNumberEntry.Text,
                    regNumber = counterRegistrationNumberEntry.Text,
                    device = new
                    {
                        id = CounterViewClass.Counter.DeviceID,
                    },
                    accounting = new
                    {
                        type = new
                        {
                            id = SelectedAccountingType.ID,
                        }
                    },
                    type = new
                    {
                        id = SelectedPUType.ID,
                    },
                    environment = new
                    {
                        type = new
                        {
                            id = SelectedEnvironment.ID,
                        }
                    },
                    state = new
                    {
                        id = SelectedCounterState.ID,
                        name = SelectedCounterState.Name
                    },
                    physicalConnection = new
                    {
                        port = IPPortEntry.Text,
                        speed = Convert.ToInt32(speedEntry.Text),
                        address = addressEntry.Text != null && addressEntry.Text != "" ? Convert.ToInt32(addressEntry.Text) : 0,
                        password = passwordEntry.Text,
                        device = new
                        {
                            type = new
                            {
                                id = SelectedCounterType.ID,
                            }
                        },
                        chanel = Convert.ToInt32(chanelNumberEntry.Text),
                    }
                };
                var result = AppRepository.Counter.Update(Links.APICounterUpdate + "?counterID=" + Counter.id, Counter, true);
                loadingIndicator.IsVisible = true;
                loadingIndicator.IsEnabled = true;
                loadingIndicator.IsRunning = true;
                counterInfo.IsVisible = false;
                counterConnection.IsVisible = false;
                acceptButton.IsVisible = false;
                await Task.Delay(1000);
                if (result.Value == null)
                {
                    await Navigation.PopModalAsync();
                    MessagingCenter.Send(this, "UpdateObject", new Object());
                }
                else
                {
                    await Navigation.PopModalAsync();
                }
            }
        }

        //проверка изменений на страницах, а именно в полях ввода на странице
        private int I { get; set; }
        private void AnyEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry.IsFocused)
                I++;
        }

        private void isActiveSwitch_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var _switch = sender as Switch;
            if (_switch.IsToggled != CounterViewClass.Counter.Active)
                I++;
        }
    }
}