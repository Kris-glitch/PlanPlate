
using CommunityToolkit.Mvvm.Input;
using PlanPlate.Utils;
using PlanPlate.View;

namespace PlanPlate.ViewModels
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private readonly Data.IUserRepository _userRepository;
        public SettingsViewModel(Data.IUserRepository repository) : base(repository)
        {
            _userRepository = repository;
        }

        [RelayCommand]
        async Task Logout()
        {
            var result = _userRepository.LogOutUser();

            if (result == null) return;

            if (result.Exception != null)
            {
                OnShowError(ExceptionHandler.HandleExceptionForUI(result.Exception));
                return;
            }

            if (!result.Data)
            {
                OnShowError("Something went wrong, try again later.");
                return;
            }

            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }

        public void SubscribeToErrorEvents(Action<string> errorHandler)
        {
            ShowError += errorHandler;
        }
        public void UnsubscribeFromErrorEvents(Action<string> errorHandler)
        {
            ShowError -= errorHandler;
        }
    }
}
