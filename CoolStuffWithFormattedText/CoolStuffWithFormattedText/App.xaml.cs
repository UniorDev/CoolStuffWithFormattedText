using CoolStuffWithFormattedText.Views;
using Prism.Unity;

namespace CoolStuffWithFormattedText
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync($"{nameof(NavigationBarPage)}/{nameof(Views.MainPage)}");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<FontIconPage>();
            Container.RegisterTypeForNavigation<FormattedTextPage>();
            Container.RegisterTypeForNavigation<NavigationBarPage>();
        }
    }
}
