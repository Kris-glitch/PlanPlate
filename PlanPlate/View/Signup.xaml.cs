using PlanPlate.ViewModels;

namespace PlanPlate.View;

public partial class Signup : ContentPage
{
	public Signup(SignupViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        viewModel.SetShowErrorAction(message => DisplayAlert("Error", message, "OK"));
    }
}