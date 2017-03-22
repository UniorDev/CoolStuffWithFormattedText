using Prism.Commands;
using Prism.Mvvm;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoolStuffWithFormattedText.ViewModels
{
    public class ComboPageViewModel : BindableBase
    {
        private FormattedString _battery = new FormattedString
        {
            Spans =
            {
                new Span { Text = BatteryStatusIcons[0], FontSize = 18, FontFamily = "FontAwesome", ForegroundColor = Color.Green },
                new Span { Text = BatteryStatus[0], FontSize = 18, ForegroundColor = Color.Black }
            }
        };

        public FormattedString Battery
        {
            get { return _battery; }
            set { SetProperty(ref _battery, value); }
        }

        private static readonly string[] BatteryStatusIcons = { "\uf240", "\uf241", "\uf242", "\uf243", "\uf244" };
        private static readonly Color[] BatteryColors = { Color.Green, Color.Yellow, Color.Olive, Color.Red, Color.Black };
        private static readonly string[] BatteryStatus = { " Full", " Half", " Low", " Very low", " Dead" };
        public DelegateCommand ConsumeBatteryCommand => new DelegateCommand(OnConsumeBattery);

        public ComboPageViewModel() { }

        private void OnConsumeBattery()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                for ( var i = 1; i < BatteryStatusIcons.Length; i++ )
                {
                    Battery.Spans[0].Text = BatteryStatusIcons[i];
                    Battery.Spans[0].ForegroundColor = BatteryColors[i];
                    Battery.Spans[1].Text = BatteryStatus[i];
                    await Task.Delay(2000);
                }
                Battery.Spans[0].Text = BatteryStatusIcons[0];
                Battery.Spans[0].ForegroundColor = BatteryColors[0];
                Battery.Spans[1].Text = BatteryStatus[0];
            });
        }
    }
}
