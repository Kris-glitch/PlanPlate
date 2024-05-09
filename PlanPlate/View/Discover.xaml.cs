using PlanPlate.ViewModels;

namespace PlanPlate.View;

public partial class Discover : ContentPage
{
    private readonly DiscoverViewModel _viewModel;

    public Discover(DiscoverViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!_viewModel.InitPerformed)
        {
            await _viewModel.GetCategories();
            await _viewModel.SearchMealsByCategory("breakfast");
            _viewModel.InitPerformed = true;
        }
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
        
        await _viewModel.SearchMealsByCategory(category);
    }


}