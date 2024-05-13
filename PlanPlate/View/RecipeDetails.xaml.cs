using PlanPlate.ViewModels;

namespace PlanPlate.View;

public partial class RecipeDetails : ContentPage
{
    private readonly RecipeDetailsViewModel _viewModel;

    public RecipeDetails(RecipeDetailsViewModel viewModel)
	{
        InitializeComponent();
        Shell.SetTabBarIsVisible(this, false);
        _viewModel = viewModel;
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.GetDetails();
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