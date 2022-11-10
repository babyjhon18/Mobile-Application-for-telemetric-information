using System.Collections.Generic;

namespace INDELLAPPEnd.Models
{
    public class LocationClass : BaseItemClass
    {
        public LocationClass()
        {
            this.Objects = new List<BaseObjectClass>();
            this.Region = new BaseItemClass();
        }

        public BaseItemClass Region { get; set; }
        public List<BaseObjectClass> Objects { get; set; }
    }
}