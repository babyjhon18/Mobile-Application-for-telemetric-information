using System;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Widget;
using INDELAPPEnd.CustomControls;
using INDELAPPEnd.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedEntryWhite), typeof(RoundedEntryRendererAndroidWhite))]
namespace INDELAPPEnd.Droid
{
    [Obsolete]
    public class RoundedEntryRendererAndroidWhite : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> args)
        {
            base.OnElementChanged(args);

            if (Control != null)
            {
                if (Build.VERSION.SdkInt < BuildVersionCodes.Q)
                {
                    IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                    IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                    //// my_cursor is the xml file name which we defined above
                    JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.CustomCursorForSearchBarWhite);
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetColor(global::Android.Graphics.Color.Transparent);
                    this.Control.SetBackgroundDrawable(gd);
                    Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.ParseColor("#c2c2c2")));
                }
                else
                {
                    Control.SetTextCursorDrawable(Resource.Drawable.CustomCursorForSearchBarWhite);
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetColor(global::Android.Graphics.Color.Transparent);
                    this.Control.SetBackgroundDrawable(gd);
                    this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                    Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.ParseColor("#c2c2c2")));

                }
            }
        }
    }
}