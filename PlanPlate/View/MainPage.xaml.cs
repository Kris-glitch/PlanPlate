using PlanPlate.ViewModels;

namespace PlanPlate.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        
    }

}
