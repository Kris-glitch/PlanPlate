using PlanPlate.ViewModels;

namespace PlanPlate.View;

public partial class Plan : ContentPage
{
    private readonly PlanViewModel _viewModel;
    public Plan(PlanViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
        _viewModel.SubscribeToErrorEvents(OnShowError);

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!_viewModel.InitPerformed)
        {

            await _viewModel.GetAllRecipesFromPlanner();

            _viewModel.InitPerformed = true;
        }
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
