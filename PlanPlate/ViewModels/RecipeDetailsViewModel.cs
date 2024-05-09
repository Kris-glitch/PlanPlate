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
    public enum RecipeFrom
    {
        Cookbook,
        OnlineApi
    }

    [QueryProperty("RecipeId", "recipeId")]
    public partial class RecipeDetailsViewModel : BaseViewModel
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICookbookRepository _cookbookRepository;
        private readonly IUserRepository _userRepository;
        public RecipeDetailsViewModel(IUserRepository userRepository, ICookbookRepository cookbookRepository, IRecipeRepository recipeRepository) : base(userRepository)
        {
            _recipeRepository = recipeRepository;
            _cookbookRepository = cookbookRepository;
            _userRepository = userRepository;
        }

        [ObservableProperty]
        private ActionType? listType;

        [ObservableProperty]
        private RecipeFrom? recipeType;

        [ObservableProperty]
        public DataOrException<MyRecipe, Exception>? recipe;

        [ObservableProperty]
        string? recipeId;

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
                    RecipeType = RecipeFrom.OnlineApi;

                    var response = await _recipeRepository.GetRecipeDetails(RecipeId);

                    if (response.Data == null && response.Exception == null)
                    {
                        var userId = GetUserId();
                        if (userId == null) return;

                        RecipeType = RecipeFrom.Cookbook;

                        response = await _cookbookRepository.SearchRecipeByIdFromCookbookAsync(userId, RecipeId);
                    }

                    Recipe.Data = response.Data;
                    Recipe.Exception = response.Exception;
                }
                finally
                {
                    Recipe.Loading = false;
                    OnPropertyChanged(nameof(Recipe));
                    OnPropertyChanged(nameof(RecipeType));
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

        private string? GetUserId()
        {
            var user = _userRepository.IsUserLoggedIn().Data;

            if (user == null)
            {
                OnShowError("Something went wrong. Please try again later");
                return null;
            }
            return user.Id;

        }
    }

}
