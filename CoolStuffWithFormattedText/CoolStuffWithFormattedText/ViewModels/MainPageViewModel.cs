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
        public DelegateCommand ComboTappedCommand => new DelegateCommand(OnComboTapped);
        public DelegateCommand UsefulTappedCommand => new DelegateCommand(OnUsefulTapped);

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

        private void OnComboTapped()
        {
            _navigationService.NavigateAsync($"{nameof(ComboPage)}");
        }

        private void OnUsefulTapped()
        {
            _navigationService.NavigateAsync($"{nameof(UsefulPage)}");
        }
    }
}
