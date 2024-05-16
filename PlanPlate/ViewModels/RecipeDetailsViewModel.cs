using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanPlate.Data;
using PlanPlate.Data.Model;
using PlanPlate.Utils;
using PlanPlate.View;

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
        private readonly IPlannerRepository _plannerRepository;
        public RecipeDetailsViewModel (IUserRepository userRepository, ICookbookRepository cookbookRepository, IRecipeRepository recipeRepository, IPlannerRepository plannerRepository) : base(userRepository)
        {
            _recipeRepository = recipeRepository;
            _cookbookRepository = cookbookRepository;
            _userRepository = userRepository;
            _plannerRepository = plannerRepository;
            IsAddToPlannerVisible = false;

            SelectedDate = DateTime.Now;

            DropDownCategories = Enum.GetValues(typeof(PlannerCategory))
                                      .Cast<PlannerCategory>()
                                      .Select(category => category.ToString())
                                      .ToList();
        }

        [ObservableProperty]
        List<string> dropDownCategories;

        [ObservableProperty]
        string? errorMessage;

        [ObservableProperty]
        private ActionType? listType;

        [ObservableProperty]
        private RecipeFrom? recipeType;

        [ObservableProperty]
        public DataOrException<MyRecipe, Exception>? recipe;

        [ObservableProperty]
        string? recipeId;

        [ObservableProperty]
        DateTime selectedDate;

        [ObservableProperty]
        string? selectedCategory;

        [ObservableProperty]
        bool isAddToPlannerVisible;

        [RelayCommand]
        private void OpenAddPopup()
        {
            IsAddToPlannerVisible = true;
            OnPropertyChanged(nameof(IsAddToPlannerVisible));
        }

        [RelayCommand]
        private void HidePopup()
        {
            IsAddToPlannerVisible = false;
            OnPropertyChanged(nameof(IsAddToPlannerVisible));
        }

        [RelayCommand]
        private async Task AddRecipeToPlanner()
        {
            if (SelectedCategory == null)
            {
                OnShowError("Please select category and a date");
                return;
            }

            var userId = GetUserId();
            if (userId == null) return;

            if (Recipe != null)
            {
                try
                {
                    if (Recipe.Data == null) return;

                    await _plannerRepository.SaveRecipeToPlannerAsync(userId, SelectedDate, SelectedCategory, Recipe.Data);

                }
                catch (Exception ex)
                {
                    OnShowError(ExceptionHandler.HandleExceptionForUI(ex));
                }
                finally
                {
                    IsAddToPlannerVisible = false;
                    OnPropertyChanged(nameof(IsAddToPlannerVisible));
                }
            }

        }

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

                    if (response != null)
                    {
                         if (response.Data == null && response.Exception == null)
                        {
                            var userId = GetUserId();
                            if (userId == null) return;

                            RecipeType = RecipeFrom.Cookbook;

                            response = await _cookbookRepository.SearchRecipeByIdFromCookbookAsync(userId, RecipeId);
                        }

                        if (response.Exception != null)
                        {
                            ErrorMessage = ExceptionHandler.HandleExceptionForUI(response.Exception);
                        }

                        Recipe.Data = response.Data;
                        Recipe.Exception = response.Exception;
                    }

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

        [RelayCommand]
        async Task ShareRecipe()
        {
            if (Recipe == null) return;
            if (Recipe.Data == null) return;

            var text = RecipeFormater.FormatRecipe(Recipe.Data);

            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Your Recipe"
            });
        }

        [RelayCommand]
        async Task UpdateRecipe()
        {
            if (Recipe == null) return;
            if (Recipe.Data == null) return;

            await Shell.Current.GoToAsync($"{nameof(AddRecipe)}", new Dictionary<string, object>
            {
                {"Recipe", Recipe.Data}
            });
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

        public void SubscribeToErrorEvents(Action<string> errorHandler)
        {
            ShowError += errorHandler;
        }
        public void UnsubscribeFromErrorEvents(Action<string> errorHandler)
        {
            ShowError -= errorHandler;
        }
    }

}
