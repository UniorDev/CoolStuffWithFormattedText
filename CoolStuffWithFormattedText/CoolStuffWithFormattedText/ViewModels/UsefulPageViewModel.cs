using Prism.Mvvm;
using System;
using Xamarin.Forms;

namespace CoolStuffWithFormattedText.ViewModels
{
    public class UsefulPageViewModel : BindableBase
    {
        public FormattedString NewLineText => new FormattedString
        {
            Spans =
            {
                new Span
                {
                    Text = "\uf063" + Environment.NewLine + "Stop looking at me",
                    FontFamily = "FontAwesome",
                    FontSize = 24
                }
            }
        };

        public FormattedString CoffeeText => new FormattedString
        {
            Spans =
            {
                new Span
                {
                    Text = "☕"
                }
            }
        };

        public FormattedString ParagraphText => new FormattedString
        {
            Spans =
            {
                new Span
                {
                    Text = "\u2029 Very long text that will take a hole line and will be truncated"
                }
            }
        };


        public UsefulPageViewModel()
        {

        }
    }
}
