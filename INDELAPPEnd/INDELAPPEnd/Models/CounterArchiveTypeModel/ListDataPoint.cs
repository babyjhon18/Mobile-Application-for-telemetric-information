using INDELAPPEnd.Helpers;
using System.Collections.Generic;

namespace INDELAPPEnd.Models.CounterArchiveTypeModel
{
    public class ListDataPoint
    {
        public ArchiveType ArchiveType { get; set; }
        public List<DataPointClass> Points { get; set; }
        public ListDataPoint()
        {
            Points = new List<DataPointClass>();
        }
    }
}
