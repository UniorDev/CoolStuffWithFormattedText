using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace CoolStuffWithFormattedText.Droid
{
    public class CustomTypefaceSpan : MetricAffectingSpan
    {
        private readonly Typeface _typeface;
        private readonly TextView _textView;
        private Font _font;

        public CustomTypefaceSpan(TextView textView, Label label, Font font)
        {
            _textView = textView;
            _font = font;
            var fontName = GetFontName(_font.FontFamily ?? label.FontFamily, _font.FontAttributes);
            try
            {
                _typeface = Typeface.CreateFromAsset(Forms.Context.Assets, fontName + ".ttf");
            }
            catch ( Java.Lang.RuntimeException exception )
            {
                if ( exception.Message == "native typeface cannot be made" ||
                    exception.Message == "Font asset not found FontAwesome.ttf" )
                    _typeface = Typeface.CreateFromAsset(Forms.Context.Assets, fontName + ".otf");
                else
                    throw;
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

        public override void UpdateDrawState(TextPaint paint)
        {
            ApplyCustomTypeFace(paint);
        }

        public override void UpdateMeasureState(TextPaint paint)
        {
            ApplyCustomTypeFace(paint);
        }

        private void ApplyCustomTypeFace(Paint paint)
        {
            paint.SetTypeface(_typeface);
            paint.TextSize = TypedValue.ApplyDimension(ComplexUnitType.Sp, _font.ToScaledPixel(),
                _textView.Resources.DisplayMetrics);
        }
    }
}