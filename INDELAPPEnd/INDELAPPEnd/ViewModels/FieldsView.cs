using System.Collections.Generic;

namespace INDELAPPEnd.ViewModels
{
    public class FieldsView
    {
        public string Date { get; set; }
        public string Value { get; set; }
    }

    public class CustomIndicatorViewModel
    {
        public string IndicatorName { get; set; }
        public List<FieldsView> FieldsViews { get; set; }
        public CustomIndicatorViewModel()
        {
            FieldsViews = new List<FieldsView>();
        }
    }
}
