using CoolStuffWithFormattedText.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace CoolStuffWithFormattedText.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public DelegateCommand FontIconTappedCommand => new DelegateCommand(OnFontIconTapped);
        public DelegateCommand FormattedTextTappedCommand => new DelegateCommand(OnFormattedTextTapped);
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private void OnFontIconTapped()
        {
            _navigationService.NavigateAsync($"{nameof(FontIconPage)}");
        }

        private void OnFormattedTextTapped()
        {
            _navigationService.NavigateAsync($"{nameof(FormattedTextPage)}");
        }
    }
}
