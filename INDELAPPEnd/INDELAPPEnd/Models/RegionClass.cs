using System.Collections.Generic;

namespace INDELLAPPEnd.Models
{
    public class RegionClass : BaseItemClass
    {
        public RegionClass()
        {
            this.Locations = new List<LocationClass>();
        }

        public List<LocationClass> Locations { get; set; }
    }
}