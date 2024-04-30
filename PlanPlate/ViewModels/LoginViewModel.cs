using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanPlate.Data;
using PlanPlate.View;


namespace PlanPlate.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {

        [ObservableProperty]
        string? email;

        [ObservableProperty]
        string? password;

        public LoginViewModel(IUserRepository repository) : base(repository)
        {
            SetShowErrorAction(DisplayError);
        }

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
                DisplayError("Email and password are required.");
                return;
            }

            var response = await repository.LogInUserAsync(Email, Password);

            if (response.Exception != null)
            {
                DisplayError(response.Exception.Message);
                return;
            } else
            {
                var user = response.Data;
                if (user!= null)
                {
                    await Shell.Current.GoToAsync($"//{nameof(Home)}");
                } else
                {
                    DisplayError("Something went wrong. Please try again later.");
                    return;
                }
                
            }

        }

       
    }
}
