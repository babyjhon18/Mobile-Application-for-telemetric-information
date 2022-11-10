using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using INDELAPPEnd.CustomControls;
using INDELAPPEnd.Droid.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedDatePicker), typeof(RoundedDatePickerAndroid))]
namespace INDELAPPEnd.Droid.CustomControls
{
    [Obsolete]
    public class RoundedDatePickerAndroid : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            GradientDrawable gd = new GradientDrawable();   
            this.Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            this.Control.SetBackgroundDrawable(gd);             
        }
    }
}