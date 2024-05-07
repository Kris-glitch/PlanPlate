using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanPlate.Data;
using PlanPlate.Data.Model;
using PlanPlate.Utils;
using PlanPlate.View;


namespace PlanPlate.ViewModels
{
    public partial class CookbookViewModel : BaseViewModel
    {
        private readonly ICookbookRepository _cookbookRepository;
        public CookbookViewModel(IUserRepository userRepository, ICookbookRepository cookbookRepository) : base(userRepository)
        {
            _cookbookRepository = cookbookRepository;
        }

        [ObservableProperty]
        string? searchQuery;

        [ObservableProperty]
        DataOrException<IEnumerable<MyCategory>, Exception>? categories;

        [ObservableProperty]
        DataOrException<IEnumerable<MyMeal>, Exception>? meals;



        [RelayCommand]
        async Task GoToAddRecipe()
        {
            await Shell.Current.GoToAsync(nameof(AddRecipe));
        }
    }
}
