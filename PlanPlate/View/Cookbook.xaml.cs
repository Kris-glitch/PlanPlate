using PlanPlate.ViewModels;

namespace PlanPlate.View;

public partial class Cookbook : ContentPage
{
    private readonly CookbookViewModel _viewModel;
    public Cookbook(CookbookViewModel viewModel)
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
            await _viewModel.GetCategories();
            await _viewModel.GetAllRecipesFromCookbook();

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

    private void OnCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton button && button.IsChecked)
        {
            var category = button.Content?.ToString();
            if (!string.IsNullOrEmpty(category))
            {
                _ = HandleRadioButtonCheckedAsync(category);
            }
        }
    }

    private async Task HandleRadioButtonCheckedAsync(string category)
    {
        await _viewModel.GetRecipesByCategoryFromCookbook(category);
    }
}