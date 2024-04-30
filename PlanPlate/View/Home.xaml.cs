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

        await _viewModel.GetCategories();
        await _viewModel.SearchMealsByCategory("Breakfast");
    }
}