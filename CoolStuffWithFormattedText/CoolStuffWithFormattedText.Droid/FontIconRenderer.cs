using Android.Graphics;
using CoolStuffWithFormattedText;
using CoolStuffWithFormattedText.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FontIcon), typeof(FontIconRenderer))]
namespace CoolStuffWithFormattedText.Droid
{
    public class FontIconRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);

            if ( e.OldElement == null )
            {
                try
                {
                    Control.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets, Element.FontFamily + ".ttf");
                }
                catch ( Java.Lang.RuntimeException exception )
                {
                    if ( exception.Message == "native typeface cannot be made" || exception.Message == "Font asset not found FontAwesome.ttf" )
                        Control.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets, Element.FontFamily + ".otf");
                    else
                        throw;
                }
            }
        }
    }
}