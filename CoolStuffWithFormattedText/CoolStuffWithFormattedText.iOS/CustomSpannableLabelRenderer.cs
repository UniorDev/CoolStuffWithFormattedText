using CoolStuffWithFormattedText;
using CoolStuffWithFormattedText.iOS;
using Foundation;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomSpannableLabel), typeof(CustomSpannableLabelRenderer))]
namespace CoolStuffWithFormattedText.iOS
{
    public class CustomSpannableLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if ( e.OldElement != null ) { }
            if ( e.NewElement != null ) UpdateFormattedText();
        }

        private void UpdateFormattedText()
        {
            var text = Control?.AttributedText as NSMutableAttributedString;
            if ( text == null ) return;
            var fontFamily = Element.FontFamily;
            text.BeginEditing();
            FixBackground(text);
            if ( Element.FormattedText == null )
                FixFontAtLocation(0, text, fontFamily, Element.FontAttributes);
            else
            {
                var location = 0;
                foreach ( var span in Element.FormattedText.Spans )
                {
                    var spanFamily = span.FontFamily ?? fontFamily;
                    FixFontAtLocation(location, text, spanFamily, span.FontAttributes);
                    location += span.Text.Length;
                }
            }
            text.EndEditing();
        }

        private void FixFontAtLocation(int location, NSMutableAttributedString text, string fontFamily,
            FontAttributes fontAttributes)
        {
            if ( fontFamily == null ) return;

            NSRange range;
            var font = ( UIFont )text.GetAttribute(UIStringAttributeKey.Font, location, out range);
            var newName = GetFontName(fontFamily, fontAttributes);
            font = UIFont.FromName(newName, font.PointSize);
            text.RemoveAttribute(UIStringAttributeKey.Font, range);
            text.AddAttribute(UIStringAttributeKey.Font, font, range);
        }

        private static void FixBackground(NSMutableAttributedString text)
        {
            var str = text.Value;
            for ( var i = 0; i < str.Length; i++ )
                if ( str[i] == '\n' )
                    text.RemoveAttribute(UIStringAttributeKey.BackgroundColor, new NSRange(i, 1));
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

        private static string GetFontName(string fontFamily, FontAttributes fontAttributes)
        {
            if ( fontAttributes.HasFlag(FontAttributes.Bold) )
                return fontAttributes.HasFlag(FontAttributes.Italic)
                    ? $"{fontFamily}-BoldItalic"
                    : $"{fontFamily}-Bold";

            return fontAttributes.HasFlag(FontAttributes.Italic)
                ? $"{fontFamily}-Italic"
                : $"{fontFamily}";
        }
    }
}
