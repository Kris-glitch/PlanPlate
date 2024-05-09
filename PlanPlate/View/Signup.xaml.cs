using PlanPlate.ViewModels;

namespace PlanPlate.View;

public partial class Signup : ContentPage
{

    private readonly SignupViewModel _viewModel;
    public Signup(SignupViewModel viewModel)
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