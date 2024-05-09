using PlanPlate.ViewModels;

namespace PlanPlate.View;

public partial class AddRecipe : ContentPage
{
    private readonly AddRecipeViewModel _viewModel;
    public AddRecipe(AddRecipeViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        Shell.SetTabBarIsVisible(this, false);
        BindingContext = viewModel;

        _viewModel.SubscribeToErrorEvents(OnShowError);
        _viewModel.SubscribeToAlert(OnShowAlert);
    }

    private async void OnShowError(string errorMessage)
    {
        await DisplayAlert("Error", errorMessage, "OK");
    }
    private async void OnShowAlert(string message, Action<bool> callback)
    {
        bool result = await DisplayAlert("Confirmation", message, "Yes", "No");
        callback?.Invoke(result);
    }
    protected override bool OnBackButtonPressed()
    {
        OnShowAlert("Are you sure you want to go back? Any unsaved changes will be lost.", async result =>
        {
            if (result)
            {
                await Shell.Current.GoToAsync("..");
            }
        });
        return true;

    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

       
        _viewModel.UnsubscribeFromErrorEvents(OnShowError);
        _viewModel.UnsubscribeFromAlert(OnShowAlert);
    }
}