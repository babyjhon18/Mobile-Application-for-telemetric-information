using INDELAPPEnd.DataViewModels;
using INDELAPPEnd.Helpers;
using INDELAPPEnd.Models;
using INDELAPPEnd.Pages.ConfigurationPageToolbarPages;
using INDELAPPEnd.Pages.UtilsPage;
using INDELLAPPEnd.Models;
using INDELLAPPEnd.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages.ObjectPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigurationPage : ContentPage, IExceptionHandler
    {
        public CounterViewClass ViewCounter { get; set; }
        public ObjectViewClass ViewObject { get; set; }
        private ToolbarItem EditObjectToolbarItem { get; set; }
        private ToolbarItem UpdateObjectToolbarItem { get; set; }
        private ToolbarItem NewCounterToolbarItem { get; set; }
        private ToolbarItem DeleteObjectToolbarItem { get; set; }
        private ToolbarItem CloneObjectToolBarItem { get; set; }
        public ConfigurationPage()
        {
            InitializeComponent();
            ViewObject = new ObjectViewClass();
            MessagingCenter.Subscribe<PollStatusPage, object>(this, "UpdatePollObject", (_page, obj) =>
            {
                MessagingCenter.Send(this, "UpdateAllObject", ViewObject);
                MessagingCenter.Unsubscribe<PollStatusPage, object>(this, "UpdatePollObject");
            });
            objectConfigurationGrid.IsVisible = false;
            UpdateObjectToolbarItem = new ToolbarItem()
            {
                Text = "Обновить",
                Order = ToolbarItemOrder.Secondary,
            };
            UpdateObjectToolbarItem.Clicked += RefreshView_Refreshing;
            NewCounterToolbarItem = new ToolbarItem()
            {
                Text = "Добавить новый прибор",
                Order = ToolbarItemOrder.Secondary,
            };
            NewCounterToolbarItem.Clicked += AddNewCounter_Clicked;
            EditObjectToolbarItem = new ToolbarItem()
            {
                Text = "Редактировать данные объекта",
                Order = ToolbarItemOrder.Secondary,
            };
            EditObjectToolbarItem.Clicked += EditObject_Clicked;
            DeleteObjectToolbarItem = new ToolbarItem()
            {
                Text = "Удалить объект",
                Order = ToolbarItemOrder.Secondary,
            };
            DeleteObjectToolbarItem.Clicked += DeleteObject_Clicked;
            CloneObjectToolBarItem = new ToolbarItem()
            {
                Text = "Клонировать объект",
                Order = ToolbarItemOrder.Secondary,
            };
            CloneObjectToolBarItem.Clicked += CloneObject_Clicked;
            ToolbarItems.Add(UpdateObjectToolbarItem);
            if (Settings.claims.Contains(new KeyValuePair<string, string>("Counter", "Create")) || Settings.IsAdmin)
                ToolbarItems.Add(NewCounterToolbarItem);
            if (Settings.claims.Contains(new KeyValuePair<string, string>("Object", "Update")) || Settings.IsAdmin)
                ToolbarItems.Add(EditObjectToolbarItem);
            if (Settings.claims.Contains(new KeyValuePair<string, string>("Object", "Delete")) || Settings.IsAdmin)
                ToolbarItems.Add(DeleteObjectToolbarItem);
            if (Settings.claims.Contains(new KeyValuePair<string, string>("Object", "Create")) || Settings.IsAdmin)
                ToolbarItems.Add(CloneObjectToolBarItem);
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            counterListView.IsEnabled = true;
            base.OnAppearing();
        }

        private async void Init()
        {
            await Task.Delay(1000);
            loadingIndicator.IsVisible = false;
            loadingIndicator.IsEnabled = false;
            loadingIndicator.IsRunning = false;
            objectConfigurationGrid.IsVisible = true;
            counterListView.IsEnabled = true;
        }

        public void SetData(ObjectViewClass viewObject)
        {
            ViewObject = viewObject;
            objectConfigurationGrid.IsVisible = false;
            counterListView.IsEnabled = false;
            loadingIndicator.IsVisible = true;
            loadingIndicator.IsEnabled = true;
            loadingIndicator.IsRunning = true;
            objectConfigurationGrid.BindingContext = ViewObject.Object;
            Init();
        }

        private void GetCounter(int counterID)
        {
            var viewCounter = AppRepository.Counter.View(Links.APICounterGet
                + "?counterID=" + counterID, new CounterViewClass(), true);
            SetMessage(viewCounter.Key, viewCounter.Value);
        }

        private async void EditObject_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<EditObjectPage, object>(this, "UpdateObject", (_page, obj) =>
            {
                RefreshView_Refreshing(obj, new EventArgs());
                MessagingCenter.Unsubscribe<EditObjectPage, object>(this, "UpdateObject");
            });
            var page = new NavigationPage(new EditObjectPage(ViewObject, 1));
            await Navigation.PushModalAsync(page, true);
        }

        private async void DeleteObject_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<AcceptDeclinePage, bool>(this, "AcceptOrDecline", async (page, boolItem) =>
            {
                if (page.Result)
                {
                    Application.Current.MainPage = new NavigationPage(new ProfileSettingsPage(true));
                    await Task.Delay(1000);
                    var result = AppRepository.Object.Delete<Object>(Links.APIObjectDelete
                        + "?objectID=" + ViewObject.Object.ID, true);
                }
                MessagingCenter.Unsubscribe<AcceptDeclinePage, bool>(this, "AcceptOrDecline");
            });
            await Navigation.PushModalAsync(new AcceptDeclinePage("Подтвердить удаление?", "Ок", "Отмена", true));
        }

        private async void CloneObject_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SelectionPage(ViewObject.Object.ID), true);
        }

        private async void AddNewCounter_Clicked(object sender, EventArgs e)
        {
            GetCounter(-1);
            Task.WaitAll();
            MessagingCenter.Subscribe<EditCounterPage, object>(this, "UpdateObject", (_page, obj) =>
            {
                RefreshView_Refreshing(obj, new EventArgs());
                MessagingCenter.Unsubscribe<EditCounterPage, object>(this, "UpdateObject");
            });
            ViewCounter.Counter.DeviceID = ViewObject.Object.Device.ID;
            var page = new NavigationPage(new EditCounterPage(ViewCounter, 0));
            await Navigation.PushModalAsync(page, true);
        }

        private async void EditCounter_Clicked(object sender, EventArgs e)
        {
            if (Settings.claims.Contains(new KeyValuePair<string, string>("Counter", "Update")) || Settings.IsAdmin)
            {
                counterListView.IsEnabled = false;
                MessagingCenter.Subscribe<EditCounterPage, object>(this, "UpdateObject", (_page, obj) =>
                {
                    RefreshView_Refreshing(obj, new EventArgs());
                    MessagingCenter.Unsubscribe<EditCounterPage, object>(this, "UpdateObject");
                });
                CounterClass counter = (sender as ImageButton).Parent.BindingContext as CounterClass;
                GetCounter(counter.ID);
                ViewCounter.Counter.DeviceID = ViewObject.Object.Device.ID;
                var page = new NavigationPage(new EditCounterPage(ViewCounter, 1));
                await Navigation.PushModalAsync(page, true);
            }
        }

        private async void CounterTapped(object sender, ItemTappedEventArgs e)
        {
            if (Settings.claims.Contains(new KeyValuePair<string, string>("Counter", "ArchiveData")) || Settings.IsAdmin)
            {
                counterListView.IsEnabled = false;
                CounterClass counter = e.Item as CounterClass;
                await Navigation.PushAsync(new DataByCounterPage(counter.Name, ViewObject.Object.ID, counter.ID), true);
            }
        }

        private async void RefreshView_Refreshing(object sender, EventArgs e)
        {
            var viewObject = AppRepository.Object.View(Links.APIObjectGet +
                "?objectID=" + ViewObject.Object.ID, new List<ObjectViewClass>(), true);
            SetMessage(viewObject.Key, viewObject.Value);
            await Task.Delay(1000);
        }

        public void SetMessage<T>(HttpStatusCode code, T data)
        {
            if (data is CounterViewClass)
            {
                switch (code)
                {
                    case HttpStatusCode.OK:
                        CounterViewClass Data = data as CounterViewClass;
                        ViewCounter = Data;
                        break;
                    case HttpStatusCode.InternalServerError:
                        Navigation.PushModalAsync(new AcceptDeclinePage("Ошибка загрузки." +
                            "Не удалось загрузить счетчик. Попробуйте обновить страницу.", "Ок", "", false));
                        break;
                }
            }
            if (data is List<ObjectViewClass>)
            {
                switch (code)
                {
                    case HttpStatusCode.OK:
                        List<ObjectViewClass> Data = data as List<ObjectViewClass>;
                        ViewObject = Data.FirstOrDefault();
                        objectConfigurationGrid.BindingContext = ViewObject.Object;
                        MessagingCenter.Send(this, "UpdateAllObject", ViewObject);
                        break;
                    case HttpStatusCode.InternalServerError:
                        Navigation.PushModalAsync(new AcceptDeclinePage("Ошибка загрузки." +
                            "Не удалось загрузить объект. Попробуйте обновить страницу.", "Ок", "", false));
                        break;
                }
            }
        }
    }
}