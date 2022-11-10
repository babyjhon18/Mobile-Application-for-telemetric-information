using Xamarin.Forms;

namespace INDELAPPEnd.CustomControls
{
    public class RoundedDatePicker : DatePicker
    {
        public static readonly BindableProperty EnterTextProperty =
            BindableProperty.Create(propertyName: "Placeholder",
                returnType: typeof(string), declaringType: typeof(RoundedDatePicker),
                defaultValue: default(string));
        public string Placeholder
        {
            get;
            set;
        }
    }
}
