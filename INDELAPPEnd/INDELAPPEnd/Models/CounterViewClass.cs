using INDELLAPPEnd.Models;
using System.Collections.Generic;

namespace INDELAPPEnd.Models
{
    public class CounterViewClass
    {
        public CounterClass Counter { get; set; }
        public List<BaseItemClass> CounterTypes { get; set; }
        public List<BaseItemClass> AccountingTypes { get; set; }
        public List<BaseItemClass> DeviceTypes { get; set; }
        public List<BaseItemClass> EnvironmentTypes { get; set; }
        public List<BaseItemClass> States { get; set; }
    }
}
