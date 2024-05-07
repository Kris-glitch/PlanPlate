using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanPlate.Data;
using PlanPlate.Data.Model;

namespace PlanPlate.ViewModels
{
    public partial class AddRecipeViewModel : BaseViewModel
    {
        private readonly IRecipeRepository _recipeRepository;

        public AddRecipeViewModel(IUserRepository userRepository, IRecipeRepository recipeRepository) : base(userRepository)
        {
            _recipeRepository = recipeRepository;
        }

        [ObservableProperty]
        string? recipeName;

        [ObservableProperty]
        string? recipeInstructions;

        [ObservableProperty]
        List<Ingredient>? ingredientsList;

        [ObservableProperty]
        string? recipeImageUri;



    }
}
