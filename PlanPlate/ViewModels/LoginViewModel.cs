using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanPlate.Data.Model;
using PlanPlate.Network;
using PlanPlate.View;


namespace PlanPlate.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IUser repository;

        [ObservableProperty]
        string? email;

        [ObservableProperty]
        string? password;

        public LoginViewModel(IUser repository)
        {
            this.repository = repository;
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

            var response = await repository.LogInAsync(Email, Password);

            if (response.Exception != null)
            {
                DisplayError(response.Exception.Message);
                return;
            } else
            {
                var user = response.Data;
                if (user!= null)
                {
                    await Shell.Current.GoToAsync(nameof(MainPage), new Dictionary<string, object>
                    {
                        { nameof(MyUser), user },
                    });
                } else
                {
                    DisplayError("Something went wrong. Please try again later.");
                    return;
                }
                
            }

        }

       
    }
}
