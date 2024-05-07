using PlanPlate.ViewModels;

namespace PlanPlate.View;

public partial class AddRecipe : ContentPage
{
	public AddRecipe(AddRecipeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}