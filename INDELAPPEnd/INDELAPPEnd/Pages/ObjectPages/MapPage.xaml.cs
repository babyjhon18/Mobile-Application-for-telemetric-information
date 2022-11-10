using ictweb5.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages.ObjectPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public void SetData(MapObjectClass point)
        {
            CreatePins(point);
        }

        private void CreatePins(MapObjectClass point)
        {
            Pin pin = new Pin()
            {
                Label = point.Name,
                Type = PinType.Place,
                Position = new Position(point.XY.Y, point.XY.X),
            };
            map.Pins.Add(pin);
            var pinPosition = pin.Position;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pinPosition, Distance.FromKilometers(1.0)));
        }
    }
}