using Android.Text;
using CoolStuffWithFormattedText;
using CoolStuffWithFormattedText.Droid;
using Java.Lang;
using System.ComponentModel;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSpannableLabel), typeof(CustomSpannableLabelRenderer))]
namespace CoolStuffWithFormattedText.Droid
{
    public class CustomSpannableLabelRenderer : FontIconRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if ( e.OldElement != null ) { }
            if ( e.NewElement != null ) UpdateFormattedText();
        }

        private void UpdateFormattedText()
        {
            if ( Element?.FormattedText == null ) return;

            var extensionType = typeof(FormattedStringExtensions);
            var type = extensionType.GetNestedType("FontSpan", BindingFlags.NonPublic);
            var ss = new SpannableString(Control.TextFormatted);
            var spans = ss.GetSpans(0, ss.ToString().Length, Class.FromType(type));
            foreach ( var span in spans )
            {
                var font = ( Font )type.GetProperty("Font").GetValue(span, null);
                if ( ( font.FontFamily ?? Element.FontFamily ) != null )
                {
                    var start = ss.GetSpanStart(span);
                    var end = ss.GetSpanEnd(span);
                    var flags = ss.GetSpanFlags(span);
                    ss.RemoveSpan(span);
                    var newSpan = new CustomTypefaceSpan(Control, Element, font);
                    ss.SetSpan(newSpan, start, end, flags);
                }
            }
            Control.TextFormatted = ss;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if ( e.PropertyName == Label.FormattedTextProperty.PropertyName ||
               e.PropertyName == Label.TextProperty.PropertyName ||
               e.PropertyName == Label.FontAttributesProperty.PropertyName ||
               e.PropertyName == Label.FontProperty.PropertyName ||
               e.PropertyName == Label.FontSizeProperty.PropertyName ||
               e.PropertyName == Label.FontFamilyProperty.PropertyName ||
               e.PropertyName == Label.TextColorProperty.PropertyName )
            {
                UpdateFormattedText();
            }
        }
    }
}