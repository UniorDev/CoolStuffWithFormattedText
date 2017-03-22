using Prism.Commands;
using Prism.Mvvm;
using System;
using Xamarin.Forms;

namespace CoolStuffWithFormattedText.ViewModels
{
    public class FormattedTextPageViewModel : BindableBase
    {
        private string first = "First";
        private string third;

        public FormattedString CustomText => new FormattedString
        {
            Spans =
            {
                new Span {Text = first, FontAttributes = FontAttributes.Italic, FontSize = 18},
                new Span {Text = "Second"},
                new Span {Text = third, ForegroundColor = Color.Green, BackgroundColor = Color.Gray}
            }
        };

        private FormattedString _dynamicFormattedText = new FormattedString { Spans = { new Span { Text = "0", FontSize = 18 } } };
        public FormattedString DynamicFormattedText
        {
            get { return _dynamicFormattedText; }
            set { SetProperty(ref _dynamicFormattedText, value); }
        }

        public DelegateCommand AddSpanCommand => new DelegateCommand(OnAddSpan);

        private readonly Span _zeroSpan = new Span { Text = "0", FontSize = 18 };

        public FormattedTextPageViewModel()
        {
            third = "Third";
        }

        private void OnAddSpan()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if ( DynamicFormattedText.Spans.Count >= 1 )
                {
                    DynamicFormattedText.Spans[DynamicFormattedText.Spans.Count - 1].Text = "1";
                    DynamicFormattedText.Spans[DynamicFormattedText.Spans.Count - 1].ForegroundColor =
                        Color.FromHex($"#{new Random().Next(0x1000000):X6}");
                }
                DynamicFormattedText.Spans.Add(_zeroSpan);
            });

        }
    }
}
