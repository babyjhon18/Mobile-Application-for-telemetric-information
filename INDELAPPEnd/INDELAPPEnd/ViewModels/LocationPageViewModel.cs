using INDELLAPPEnd.Models;
using System.Collections.Generic;

namespace INDELAPPEnd.ViewModels
{
    public class LocationPageViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public LocationPageViewModel()
        {
        }

        public LocationPageViewModel(CustomRegionClass region)
        {
            Title = region.name;
            Locations = region.Locations;
        }

        private List<CustomLocationClass> _locations;
        public List<CustomLocationClass> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                OnPropertyChanged("Regions");
            }
        }
    }
}
