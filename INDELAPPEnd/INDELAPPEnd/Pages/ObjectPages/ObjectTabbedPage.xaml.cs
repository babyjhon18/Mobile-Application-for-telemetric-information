using ictweb5.Models;
using INDELAPPEnd.DataViewModels;
using INDELAPPEnd.Helpers;
using INDELAPPEnd.Pages.UtilsPage;
using INDELLAPPEnd.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages.ObjectPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObjectTabbedPage : TabbedPage, IExceptionHandler
    {
        public ObservableCollection<CustomRegionClass> Regions = new ObservableCollection<CustomRegionClass>();
        private ObjectViewClass ViewObject { get; set; }
        private MapObjectClass Point = new MapObjectClass();
        private int ObjectId { get; set; }

        public ObjectTabbedPage(int id, string name, ObservableCollection<CustomRegionClass> regions)
        {
            InitializeComponent();
            MessagingCenter.Subscribe<ConfigurationPage, ObjectViewClass>(this, "UpdateAllObject", (_page, _object) =>
            {
                List<ObjectViewClass> objectView = new List<ObjectViewClass>();
                objectView.Add(_object);
                SetMessage(HttpStatusCode.OK, objectView);
                MessagingCenter.Unsubscribe<ConfigurationPage, object>(this, "UpdateAllObject");
            });
            Title = name;
            ObjectId = id;
            Regions = regions;
            GetObjectData();
            BindingContext = this;
        }

        public void GetObjectData()
        {
            var viewObject = AppRepository.Object.View(Links.APIObjectGet +
                "?objectID=" + ObjectId, new List<ObjectViewClass>(), true);
            var objectIDMap = viewObject.Value.FirstOrDefault().Object.ID;
            Point = AppRepository.Map.View(Links.APIMap +
                "?objectID=" + objectIDMap, new MapObjectClass(), true).Value;
            if (Point == null)
                objectTabbedPage.Children.Remove(mapPage);
            else if (!Settings.IsAdmin)
            {
                if (!Settings.claims.Contains(new KeyValuePair<string, string>("Map", "Index")))
                    objectTabbedPage.Children.Remove(mapPage);
                if (Point != null)
                    mapPage.SetData(Point);
            }
            else
                mapPage.SetData(Point);
            SetMessage(viewObject.Key, viewObject.Value);
        }

        public void SetMessage<T>(HttpStatusCode code, T data)
        {
            switch (code)
            {
                case HttpStatusCode.OK:
                    List<ObjectViewClass> Data = data as List<ObjectViewClass>;
                    ViewObject = Data.FirstOrDefault();
                    ViewObject.ObjectTree = Regions;
                    configurationPage.SetData(ViewObject);
                    dataPage.SetData(ViewObject);
                    pollPage.SetData(ViewObject);
                    if (!Settings.IsAdmin)
                    {
                        if (!Settings.claims.Contains(new KeyValuePair<string, string>("Schedule", "ReadCurrent"))
                        && !Settings.claims.Contains(new KeyValuePair<string, string>("Schedule", "ReadArchive")))
                            objectTabbedPage.Children.Remove(pollPage);
                    }
                    if (!Settings.IsAdmin)
                    {
                        if (!Settings.claims.Contains(new KeyValuePair<string, string>("Counter", "ArchiveData")))
                            objectTabbedPage.Children.Remove(dataPage);
                    }
                    Title = ViewObject.Object.Name;
                    break;
                case HttpStatusCode.InternalServerError:
                    Navigation.PushModalAsync(new AcceptDeclinePage("Ошибка загрузки." +
                        "Не удалось загрузить объект. Попробуйте обновить страницу.", "Ок", "", false));
                    break;
            }
        }
    }
}