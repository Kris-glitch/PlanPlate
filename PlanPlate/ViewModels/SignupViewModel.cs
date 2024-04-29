using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanPlate.Data.Model;
using PlanPlate.Network;
using PlanPlate.View;

namespace PlanPlate.ViewModels
{
    public partial class SignupViewModel : BaseViewModel
    {
        private readonly IUser repository;

        public SignupViewModel(IUser repository)
        {
            this.repository = repository;
            SetShowErrorAction(DisplayError);
        }

        [ObservableProperty]
        string? email;

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
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                DisplayError("Email and password are required.");
                return;
            }

            if (Password != ConfirmPassword)
            {
                DisplayError("Password doesn't match.");
                return;
            }

            var response = await repository.SignUpAsync(Email, Password);

            if (response.Exception != null)
            {
                DisplayError(response.Exception.Message);
                return;
            }
            else
            {
                var user = response.Data;

                if (user != null)
                {
                    await Shell.Current.GoToAsync(nameof(MainPage), new Dictionary<string, object>
                    {
                        { nameof(MyUser), user },
                    });
                }
                else
                {
                    DisplayError("Something went wrong. Please try again later.");
                    return;
                }
                
            }
        }
    }
}
