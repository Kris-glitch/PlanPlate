using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanPlate.Data;
using PlanPlate.View;

namespace PlanPlate.ViewModels
{
    public partial class SignupViewModel(IUserRepository repository) : BaseViewModel(repository)
    {
        [ObservableProperty]
        string? email;

        [ObservableProperty]
        string? username;

        [ObservableProperty]
        string? password;

        [ObservableProperty]
        string? confirmPassword;

        [RelayCommand]
        async Task GoToLogin()
        {
            await Shell.Current.GoToAsync("..");
        }


        [RelayCommand]
        async Task Signup()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) 
                || string.IsNullOrWhiteSpace(ConfirmPassword) || string.IsNullOrWhiteSpace(Username))
            {
                OnShowError("Username, email and password are required.");
                return;
            }

            if (Password != ConfirmPassword)
            {
                OnShowError("Password doesn't match.");
                return;
            }

            var response = await repository.SignUpUserAsync(Email, Password, Username);

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
