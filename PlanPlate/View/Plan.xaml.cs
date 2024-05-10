using PlanPlate.ViewModels;

namespace PlanPlate.View;

public partial class Plan : ContentPage
{
	public Plan(PlanViewModel viewModel)
	{
		InitializeComponent();
		BindingContext=viewModel;
	}
}