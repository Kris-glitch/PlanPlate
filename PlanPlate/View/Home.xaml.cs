using PlanPlate.ViewModels;

namespace PlanPlate.View;

public partial class Home : ContentPage
{
    private readonly HomeViewModel _viewModel;

    public Home(HomeViewModel viewModel)
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
}