using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanPlate.Data;
using PlanPlate.View;


namespace PlanPlate.ViewModels
{
    public partial class LoginViewModel(IUserRepository repository) : BaseViewModel(repository)
    {

        [ObservableProperty]
        string? email;

        [ObservableProperty]
        string? password;

        [RelayCommand]
        async Task GoToSignup()
        {
            await Shell.Current.GoToAsync(nameof(Signup));
        }

        [RelayCommand]
        async Task Login()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                OnShowError("Email and password are required.");
                return;
            }

            var response = await repository.LogInUserAsync(Email, Password);

            if (response.Exception != null)
            {
                OnShowError(response.Exception.Message);
                return;
            }
            else
            {
                var user = response.Data;
                if (user != null)
                {
                    await Shell.Current.GoToAsync($"//{nameof(Discover)}");
                }
                else
                {
                    OnShowError("Something went wrong. Please try again later.");
                    return;
                }

            }

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
