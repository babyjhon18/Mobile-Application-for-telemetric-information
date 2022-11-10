using INDELAPPEnd.DataViewModels;
using INDELAPPEnd.Helpers;
using INDELAPPEnd.Pages.UtilsPage;
using INDELLAPPEnd.Models;
using INDELLAPPEnd.Pages;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages.ConfigurationPageToolbarPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditObjectPage : ContentPage
    {
        public EditObjectPage()
        {
            InitializeComponent();
        }

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
                                EditObjectButtonClick(new object(), new EventArgs());
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

        private bool isVisibleIPPort;
        public bool IsVisibleIPPort
        {
            get { return isVisibleIPPort; }
            set
            {
                isVisibleIPPort = value;
                OnPropertyChanged("IsVisibleIPPort");
            }
        }

        private bool isVisibleLocalPort;
        public bool IsVisibleLocalPort
        {
            get { return isVisibleLocalPort; }
            set
            {
                isVisibleLocalPort = value;
                OnPropertyChanged("IsVisibleLocalPort");
            }
        }

        private bool isVisibleEthernet;
        public bool IsVisibleEthernet
        {
            get { return isVisibleEthernet; }
            set
            {
                isVisibleEthernet = value;
                OnPropertyChanged("IsVisibleEthernet");
            }
        }

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

        private bool isVisibleGSM;
        public bool IsVisibleGSM
        {
            get { return isVisibleGSM; }
            set
            {
                isVisibleGSM = value;
                OnPropertyChanged("IsVisibleGSM");
            }
        }
        private BaseItemClass SelectedObjectType = new BaseItemClass();
        private BaseItemClass SelectedConectionType = new BaseItemClass();
        private CustomLocationClass SelectedLocation = new CustomLocationClass();
        private ParentedContactClass SelectedConsumer = new ParentedContactClass();
        private int PageType { get; set; }
        private ObjectViewClass ViewObject { get; set; }

        //pageType = 1 - edit,pageType =  0 - new
        public EditObjectPage(ObjectViewClass viewObject, int pageType)
        {
            ViewObject = viewObject;
            InitializeComponent();
            objectInfo.IsVisible = true;
            objectParams.IsVisible = true;
            acceptButton.IsVisible = true;
            I = 0;
            PageType = pageType;
            if (pageType == 0)
                TitleOfPage = "Добавить объект";
            else
                TitleOfPage = "Редактировать объект";
            ViewObject.Consumers.Insert(0, new ParentedContactClass() { Name = "Не выбрано" });
            ViewObject.ObjectTypes.Insert(0, new BaseItemClass() { Name = "Не выбрано" });
            if (pageType == 0)
            {
                objectNameEntry.Text = "";
                houseCodeEntry.Text = "";
                locationEntry.Text = "";
                SelectedObjectType = ViewObject.ObjectTypes.FirstOrDefault();
                objectTypeEntry.Text = SelectedObjectType.Name;
                SelectedConsumer = ViewObject.Consumers.FirstOrDefault();
                consumerEntry.Text = SelectedConsumer.Name;
                SelectedConectionType = ViewObject.ConnectionTypes.FirstOrDefault();
                connectionTypeEntry.Text = SelectedConectionType.Name;
                ethernetEntry.Text = "";
                IPPortEntry.Text = "";
                localPortEntry.Text = "";
                GSMEntry.Text = "";
            }
            else
            {
                objectNameEntry.Text = ViewObject.Object.Name;
                houseCodeEntry.Text = ViewObject.Object.HouseCode;
                SelectedLocation.id = ViewObject.Object.Location.ID;
                SelectedLocation.name = ViewObject.Object.Location.Name;
                SelectedConsumer = ViewObject.Consumers.Where(c => c.ID == ViewObject.Object.Consumer.ID).FirstOrDefault();
                if (ViewObject.Object.Device.Connection.Type == 0)
                    SelectedConectionType = ViewObject.ConnectionTypes.FirstOrDefault();
                else
                    SelectedConectionType = ViewObject.ConnectionTypes.Where(con =>
                            con.ID == Convert.ToInt32(ViewObject.Object.Device.Connection.Type)).FirstOrDefault();
                SelectedObjectType = ViewObject.ObjectTypes.Where(o => o.ID == ViewObject.Object.Type.ID).FirstOrDefault();
                locationEntry.Text = ViewObject.Object.Location.Name;
                consumerEntry.Text = ViewObject.Consumers.Where(c => c.ID == SelectedConsumer.ID).FirstOrDefault().Name;
                connectionTypeEntry.Text = ViewObject.ConnectionTypes.Where(con =>
                            con.ID == SelectedConectionType.ID).FirstOrDefault().Name;
                objectTypeEntry.Text = ViewObject.ObjectTypes.Where(o => o.ID == SelectedObjectType.ID).FirstOrDefault().Name;
                GSMEntry.Text = ViewObject.Object.Device.Connection.PhoneNumber;
                ethernetEntry.Text = ViewObject.Object.Device.Connection.IPAddress;
                IPPortEntry.Text = ViewObject.Object.Device.Connection.IPPort;
                localPortEntry.Text = ViewObject.Object.Device.Connection.LocalPort;
            }
            isActiveSwitch.IsToggled = ViewObject.Object.Active;
            SetConnectionTypeEntry(connectionTypeEntry.Text);
            RTUEntry.Text = ViewObject.Object.Device.RTU.ToString();
            TapGestureRecognizer locationTap = new TapGestureRecognizer();
            locationTap.Tapped += LocationEntry_Tapped;
            locationFrame.GestureRecognizers.Add(locationTap);
            TapGestureRecognizer consumerTap = new TapGestureRecognizer();
            consumerTap.Tapped += ConsumerEntry_Tapped;
            consumerFrame.GestureRecognizers.Add(consumerTap);
            TapGestureRecognizer objectTypeTap = new TapGestureRecognizer();
            objectTypeTap.Tapped += ObjectTypeEntry_Tapped;
            objectTypeFrame.GestureRecognizers.Add(objectTypeTap);
            TapGestureRecognizer connectionType = new TapGestureRecognizer();
            connectionType.Tapped += ConnectionType_Tapped;
            connectionTypeFrame.GestureRecognizers.Add(connectionType);
            BindingContext = this;
        }

        private void SetConnectionTypeEntry(string connectionType)
        {
            switch (connectionType)
            {
                case "Indel OPC Server":
                    IsVisibleGSM = false;
                    IsVisibleEthernet = false;
                    IsVisibleIPPort = false;
                    IsVisibleLocalPort = false;
                    break;
                case "MEK-104/UDP":
                    IsVisibleGSM = false;
                    IsVisibleEthernet = true;
                    IsVisibleIPPort = true;
                    IsVisibleLocalPort = true;
                    return;
                case "Modem (AX Socket)":
                    IsVisibleGSM = true;
                    IsVisibleEthernet = false;
                    IsVisibleIPPort = false;
                    IsVisibleLocalPort = false;
                    return;
                case "TCP/IP":
                    IsVisibleGSM = false;
                    IsVisibleEthernet = true;
                    IsVisibleIPPort = true;
                    IsVisibleLocalPort = true;
                    return;
                case "TCP/IP (Indel протокол)":
                    IsVisibleGSM = false;
                    IsVisibleEthernet = true;
                    IsVisibleIPPort = true;
                    IsVisibleLocalPort = true;
                    return;
                case "UDP":
                    IsVisibleGSM = false;
                    IsVisibleEthernet = true;
                    IsVisibleIPPort = true;
                    IsVisibleLocalPort = true;
                    return;
                case "МЭК-104":
                    IsVisibleGSM = false;
                    IsVisibleEthernet = true;
                    IsVisibleIPPort = true;
                    IsVisibleLocalPort = true;
                    return;
            }
        }

        private async void LocationEntry_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<ListViewPage, CustomLocationClass>(this, "SetLocation", (page, location) =>
            {
                I++;
                SelectedLocation = location;
                locationEntry.Text = location.name;
                MessagingCenter.Unsubscribe<ListViewPage, CustomLocationClass>(this, "SetLocation");
            });
            await Navigation.PushModalAsync(new ListViewPage(ViewObject.ObjectTree, 0, locationEntry.Text));
        }

        private async void ConsumerEntry_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<ListViewPage, ParentedContactClass>(this, "SetConsumer", (page, consumer) =>
            {
                I++;
                SelectedConsumer = consumer;
                if (consumer.Name == "Не выбрано")
                    consumerEntry.Text = "Не выбрано";
                else
                    consumerEntry.Text = consumer.Name;
                MessagingCenter.Unsubscribe<ListViewPage, ParentedContactClass>(this, "SetConsumer");
            });
            await Navigation.PushModalAsync(new ListViewPage(ViewObject.Consumers, 2,
                consumerEntry.Text == "" || consumerEntry.Text == null ? "Не выбрано" : consumerEntry.Text));
        }

        private async void ObjectTypeEntry_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<ListViewPage, BaseItemClass>(this, "SetObjectType", (page, objectType) =>
            {
                I++;
                SelectedObjectType = objectType;
                if (objectType.Name == "Не выбрано")
                    objectTypeEntry.Text = "Не выбрано";
                else
                    objectTypeEntry.Text = objectType.Name;
                MessagingCenter.Unsubscribe<ListViewPage, BaseItemClass>(this, "SetObjectType");
            });
            await Navigation.PushModalAsync(new ListViewPage(ViewObject.ObjectTypes, 1,
                objectTypeEntry.Text == "" || objectTypeEntry == null ? "Не выбрано" : objectTypeEntry.Text));
        }

        private async void ConnectionType_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<ListViewPage, BaseItemClass>(this, "SetConnectionType", (page, connection) =>
            {
                I++;
                SelectedConectionType = connection;
                connectionTypeEntry.Text = connection.Name;
                SetConnectionTypeEntry(connection.Name);
                MessagingCenter.Unsubscribe<ListViewPage, BaseItemClass>(this, "SetConnectionType");
            });
            await Navigation.PushModalAsync(new ListViewPage(ViewObject.ConnectionTypes, 3, connectionTypeEntry.Text));
        }

        private async void EditObjectButtonClick(object sender, EventArgs e)
        {
            var phone = GSMEntry.Text != "" && GSMEntry.Text != null ? GSMEntry.Text : "";
            var ethernet = ethernetEntry.Text != "" && ethernetEntry.Text != null ? ethernetEntry.Text : "";
            int ipPort;
            int localPort;
            int.TryParse(IPPortEntry.Text, out ipPort);
            int.TryParse(localPortEntry.Text, out localPort);
            if (PageType == 0)
            {
                var Object = new
                {
                    name = objectNameEntry.Text,
                    code = "",
                    house = "",
                    building = "",
                    houseCode = houseCodeEntry.Text != "" && houseCodeEntry.Text != null ? houseCodeEntry.Text : "",
                    active = isActiveSwitch.IsToggled,
                    group = new
                    {
                        id = -1,
                    },
                    street = new
                    {
                        id = -1,
                    },
                    consumer = new
                    {
                        id = SelectedConsumer.ID,
                    },
                    kind = new
                    {
                        id = -1,
                    },
                    additionalInfo = new
                    {
                        data = "",
                    },
                    location = new
                    {
                        id = SelectedLocation.id,
                    },
                    type = new
                    {
                        id = SelectedObjectType.ID,
                    },
                    device = new
                    {
                        name = "Контроллер Индел",
                        rtu = RTUEntry.Text,
                        connection = new
                        {
                            type = SelectedConectionType.ID,
                            phoneNumber = phone,
                            ipAddress = ethernet,
                            ipPort = ipPort,
                            localPort = localPort,
                        }
                    }
                };
                if (Object.name == "" || Object.name == null)
                    await Navigation.PushModalAsync(new AcceptDeclinePage("Ошибка заполнения. " +
                        "Поле названия не может быть пустым.", "Ок", "", false));
                else if (SelectedLocation.id == 0 || SelectedLocation.name == null)
                    await Navigation.PushModalAsync(new AcceptDeclinePage("Ошибка заполнения. Выберете место.",
                        "Ок", "", false));
                else if (ethernetEntry.IsVisible && (ethernetEntry.Text == null || ethernetEntry.Text == ""))
                    await Navigation.PushModalAsync(new AcceptDeclinePage("Ошибка заполнения. " +
                        "Поле Ethernet адреса не может быть пустым.", "Ок", "", false));
                else if (GSMEntry.IsVisible && (GSMEntry.Text == null || GSMEntry.Text == ""))
                    await Navigation.PushModalAsync(new AcceptDeclinePage("Ошибка заполнения" +
                        "Поле номера GSM не может быть пустым.", "Ок", "", false));
                else
                {
                    AppRepository.Object.Create<CommonObjectClass>(Links.APIObjectCreate, Object, true);
                    Application.Current.MainPage = new NavigationPage(new ProfileSettingsPage(true));
                }
            }
            else
            {
                var Object = new
                {
                    id = ViewObject.Object.ID,
                    name = objectNameEntry.Text,
                    code = "",
                    house = "",
                    building = "",
                    houseCode = houseCodeEntry.Text != "" && houseCodeEntry.Text != null ? houseCodeEntry.Text : "",
                    active = isActiveSwitch.IsToggled,
                    group = new
                    {
                        id = -1,
                    },
                    street = new
                    {
                        id = -1,
                    },
                    consumer = new
                    {
                        id = SelectedConsumer.ID,
                    },
                    kind = new
                    {
                        id = -1,
                    },
                    additionalInfo = new
                    {
                        data = "",
                    },
                    location = new
                    {
                        id = SelectedLocation.id,
                    },
                    type = new
                    {
                        id = SelectedObjectType.ID,
                    },
                    device = new
                    {
                        id = ViewObject.Object.Device.ID,
                        name = "Контроллер Индел",
                        rtu = RTUEntry.Text,
                        connection = new
                        {
                            type = SelectedConectionType.ID,
                            phoneNumber = phone,
                            ipAddress = ethernet,
                            ipPort = ipPort,
                            localPort = localPort,
                        }
                    }
                };
                if (Object.name == "" || Object.name == null)
                    await Navigation.PushModalAsync(new AcceptDeclinePage("Ошибка заполнения. " +
                        "Поле названия не может быть пустым.", "Ок", "", false));
                else if (SelectedLocation.id == 0 || SelectedLocation.name == null)
                    await Navigation.PushModalAsync(new AcceptDeclinePage("Ошибка заполнения. Выберете место.",
                        "Ок", "", false));
                else if (ethernetEntry.IsVisible && (ethernetEntry.Text == null || ethernetEntry.Text == ""))
                    await Navigation.PushModalAsync(new AcceptDeclinePage("Ошибка заполнения. " +
                        "Поле Ethernet адреса не может быть пустым.", "Ок", "", false));
                else if (GSMEntry.IsVisible && (GSMEntry.Text == null || GSMEntry.Text == ""))
                    await Navigation.PushModalAsync(new AcceptDeclinePage("Ошибка заполнения" +
                        "Поле номера GSM не может быть пустым.", "Ок", "", false));
                else
                {
                    AppRepository.Object.Update<Object>(Links.APIObjectUpdate + "?objectID=" + Object.id, Object, true);
                    Application.Current.MainPage = new NavigationPage(new ProfileSettingsPage(true));
                }
            };
        }

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
            if (_switch.IsToggled != ViewObject.Object.Active)
                I++;
        }
    }
}