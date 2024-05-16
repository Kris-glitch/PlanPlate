using PlanPlate.Data.Model;
using PlanPlate.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanPlate.View;
using System.Collections.ObjectModel;
using PlanPlate.Utils;


namespace PlanPlate.ViewModels
{
    public enum PlannerCategory
    {
        Breakfast,
        Lunch,
        Dinner,
        Dessert,
        Salad,
        Snacks,
        Other
    }

    public partial class PlanViewModel : BaseViewModel
    {

        private readonly IPlannerRepository _plannerRepository;
        private readonly IUserRepository _userRepository;

        private readonly List<string> categories = Enum.GetValues(typeof(PlannerCategory))
                                      .Cast<PlannerCategory>()
                                      .Select(category => category.ToString())
                                      .ToList();

        public PlanViewModel(IUserRepository userRepository, IPlannerRepository plannerRepository) : base(userRepository)
        {
            
            _plannerRepository = plannerRepository;
            _userRepository = userRepository;
            SelectedDate = DateTime.Now;
        }

        private bool initPerformed = false;

        public bool InitPerformed
        {
            get { return initPerformed; }
            set
            {
                if (initPerformed != value)
                {
                    initPerformed = value;
                    OnPropertyChanged(nameof(InitPerformed));
                }

            }
        }

        public ObservableCollection<RecipeGroup>? GroupOfRecipes { get; set; }

        [ObservableProperty]
        DateTime selectedDate;

        [ObservableProperty]
        bool isLoading;

        [RelayCommand]
        private async Task GoToRecipeDetails(string recipeId)
        {
            if (recipeId != null)
            {
                await Shell.Current.GoToAsync($"{nameof(RecipeDetails)}?recipeId={recipeId}");
            }
        }

        [RelayCommand]
        public async Task DeleteRecipeFromPlanner(string category)
        {
            var userId = GetUserId();
            if (userId == null) return;

            if (category == null) return;

            try
            {
                await _plannerRepository.DeleteRecipeFromPlannerAsync(userId, SelectedDate, category); 
               
            }
            catch (Exception ex)
            {
                OnShowError(ExceptionHandler.HandleExceptionForUI(ex));
            }
            finally
            {
                await GetAllRecipesFromPlanner();
            }

        }

        [RelayCommand]
        public async Task GetAllRecipesFromPlanner()
        {

            GroupOfRecipes = new ObservableCollection<RecipeGroup>();

            var userId = GetUserId();
            if (userId == null) return;

            IsLoading = true;
            
            try
            {
                foreach (var category in categories)
                {
                    var response = await _plannerRepository.GetAllRecipesFromPlanner(userId, SelectedDate, category);
                    SetDataAndExceptionForCategory(category, response.Data, response.Exception);
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        async Task CreateShoppingList()
        {
            var userId = GetUserId();
            if (userId == null) return;

            var weekDates = DateFormater.GetWeekDates(SelectedDate);

            try
            {
                IsLoading = true;
                var response = await _plannerRepository.GetWeeklyRecipesFromPlanner(userId, weekDates);

                if (response != null)
                {
                    var ingredientsList = ShoppingListMaker.GetIngredientsFromRecipes(response);
                    var shoppingList = ShoppingListMaker.CreateShoppingList(ingredientsList);

                    var formatedShoppingList = RecipeFormater.FormatIngredients(shoppingList);

                    await Share.Default.RequestAsync(new ShareTextRequest
                    {
                        Text = formatedShoppingList,
                        Title = "My weekly shopping list"
                    });
                }
                
            }
            catch (Exception ex)
            {
                OnShowError(ExceptionHandler.HandleExceptionForUI(ex));
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void SetDataAndExceptionForCategory(string category, MyRecipe? data, Exception? exception)
        {
            var recipes = new List<MyRecipe>();

            if (data != null)
            {
                recipes.Add(data);
            }

            GroupOfRecipes?.Add(new RecipeGroup(category, recipes));

            OnPropertyChanged(nameof(GroupOfRecipes));
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
