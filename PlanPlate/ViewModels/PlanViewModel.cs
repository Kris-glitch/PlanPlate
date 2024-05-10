using PlanPlate.Data.Model;
using PlanPlate.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using Plugin.Maui.Calendar.Models;
using CommunityToolkit.Mvvm.Input;


namespace PlanPlate.ViewModels
{
    
    public partial class PlanViewModel : BaseViewModel
    {

        private readonly IPlannerRepository _plannerRepository;
        private readonly IUserRepository _userRepository;

        public PlanViewModel(IUserRepository userRepository, IPlannerRepository plannerRepository) : base(userRepository)
        {
            _userRepository = userRepository;
            _plannerRepository = plannerRepository;
        }

        [ObservableProperty]
        EventCollection? plannedRecipes;

        [ObservableProperty]
        DateTime selectedDate;

        [RelayCommand]
        async Task AddRecipeToMealPlanner()
        {

        }

        [RelayCommand]
        async Task GetRecipesForPlanner()
        {

        }
    }
}
