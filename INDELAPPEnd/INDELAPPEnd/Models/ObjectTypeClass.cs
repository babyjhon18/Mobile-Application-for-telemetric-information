using INDELLAPPEnd.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace INDELAPPEnd.Models
{
    public class ObjectTypeClass : BaseItemClass
    {
        public int bActive { get; set; }
        public int TypeIntCode { get; set; }
        public string TypeFields { get; set; }
        public int FieldsCount { get; set; }
        public string AccountingTypes { get; set; }
        public int TemplateType { get; set; }
    }
}
