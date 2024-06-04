using PlanPlate.ViewModels;

namespace PlanPlate.View;

public partial class Login : ContentPage
{
    private readonly LoginViewModel _viewModel;
    public Login(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        _viewModel = viewModel;
        _viewModel.SubscribeToErrorEvents(OnShowError);

	}

    private async void OnShowError(string errorMessage)
    {
        await DisplayAlert("Error", errorMessage, "OK");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.UnsubscribeFromErrorEvents(OnShowError);
    }

}