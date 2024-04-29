using PlanPlate.ViewModels;

namespace PlanPlate.View;

public partial class Login : ContentPage
{
	public Login(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

        viewModel.SetShowErrorAction(message => DisplayAlert("Error", message, "OK"));

    }
}