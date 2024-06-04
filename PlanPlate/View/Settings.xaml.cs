using PlanPlate.ViewModels;

namespace PlanPlate.View;

public partial class Settings : ContentPage
{
    private readonly SettingsViewModel _viewModel;
    public Settings(SettingsViewModel viewModel)
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