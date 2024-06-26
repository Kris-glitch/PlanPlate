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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.CheckIfEditRecipe();
    }
        
    protected override bool OnBackButtonPressed()
    {

        Dispatcher.Dispatch(async () =>
        {
            TaskCompletionSource<bool> userResponse = new TaskCompletionSource<bool>();

            OnShowAlert("Are you sure you want to go back? Any unsaved changes will be lost.", result =>
            {
                userResponse.SetResult(result);
            });

            bool leave = await userResponse.Task;

            if (leave)
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