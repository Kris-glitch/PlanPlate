using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanPlate.Data;
using PlanPlate.Data.Model;
using PlanPlate.Utils;

namespace PlanPlate.ViewModels
{
    public enum ActionType
    {
        Ingredients,
        Instructions
    }

    [QueryProperty("RecipeId", "recipeId")]
    public partial class RecipeDetailsViewModel : BaseViewModel
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeDetailsViewModel(IUserRepository userRepository, IRecipeRepository recipeRepository) : base(userRepository)
        {
            _recipeRepository = recipeRepository;
        }

        [ObservableProperty]
        private ActionType? listType;

        [ObservableProperty]
        public DataOrException<MyRecipe, Exception>? recipe;

        [ObservableProperty]
        string? recipeId;

        [ObservableProperty]
        string? recipeName;

        [RelayCommand]
        public async Task GetDetails()
        {
            Recipe = new DataOrException<MyRecipe, Exception>
            {
                Data = null,
                Loading = true,
                Exception = null
            };

            if (RecipeId != null)
            {
                try
                {
                    var response = await _recipeRepository.GetRecipeDetails(RecipeId);

                    Recipe.Data = response.Data;
                    Recipe.Exception = response.Exception;

                }
                finally
                {
                    Recipe.Loading = false;
                    OnPropertyChanged(nameof(Recipe));

                }

            }
        }

        [RelayCommand]
        private void OnIngredientsOrPropertyClicked(ActionType receivedActionType)
        {
            
            switch (receivedActionType)
            {
                case ActionType.Ingredients:
                    ListType = receivedActionType;
                    OnPropertyChanged(nameof(ListType));
                    break;
                case ActionType.Instructions:
                    ListType = receivedActionType;
                    OnPropertyChanged(nameof(ListType));
                    break;
                default:
                    ListType = null;
                    OnPropertyChanged(nameof(ListType));
                    break;
            }

        }

    }

}
