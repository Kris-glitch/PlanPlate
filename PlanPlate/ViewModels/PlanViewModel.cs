using PlanPlate.Data.Model;
using PlanPlate.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanPlate.View;


namespace PlanPlate.ViewModels
{
    public enum PlannerCategory
    {
        Breakfast,
        Lunch,
        Dinner,
        Dessert,
        Other
    }
    
    public partial class PlanViewModel : BaseViewModel
    {

        private readonly IPlannerRepository _plannerRepository;
        private readonly IUserRepository _userRepository;

        public PlanViewModel(IUserRepository userRepository, IPlannerRepository plannerRepository) : base(userRepository)
        {
            
            _plannerRepository = plannerRepository;
            _userRepository = userRepository;
            SelectedDate = DateTime.Now;

            Categories = Enum.GetValues(typeof(PlannerCategory))
                                      .Cast<PlannerCategory>()
                                      .Select(category => category.ToString())
                                      .ToList();
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


        [ObservableProperty]
        List<string> categories;

        [ObservableProperty]
        DateTime selectedDate;

        [ObservableProperty]
        MyRecipe? breakfastRecipe;

        [ObservableProperty]
        MyRecipe? lunchRecipe;

        [ObservableProperty]
        MyRecipe? dinnerRecipe;

        [ObservableProperty]
        MyRecipe? dessertRecipe;

        [ObservableProperty]
        MyRecipe? otherRecipe;

        [ObservableProperty]
        bool isLoading;

        [ObservableProperty]
        string? categoryToDelete;

        [ObservableProperty]
        bool isLabelVisible;

        [ObservableProperty]
        bool isDataVisible;

        [RelayCommand]
        private async Task GoToRecipeDetails(string recipeId)
        {
            if (recipeId != null)
            {
                await Shell.Current.GoToAsync($"{nameof(RecipeDetails)}?recipeId={recipeId}");
            }
        }
        [RelayCommand]
        public async Task DeleteBreakfastRecipeFromPlanner(string recipeId)
        {
            var userId = GetUserId();
            if (userId == null) return;

            try
            {
                await _plannerRepository.DeleteRecipeFromPlannerAsync(userId, SelectedDate, recipeId, CategoryToDelete); //not sure for this need to recheck how to do 
               
            }
            catch (Exception ex)
            {
                OnShowError(ex.Message);
            }
            
        }

        [RelayCommand]
        public async Task GetAllRecipesFromPlanner()
        {

            var userId = GetUserId();
            if (userId == null) return;

            foreach (var category in Categories)
            {

                IsLoading = true;

                try
                {
                    var response = await _plannerRepository.GetAllRecipesFromPlanner(userId, SelectedDate, category);
                    SetDataAndExceptionForCategory(category, response.Data, response.Exception);
                }
                finally
                {
                    IsLoading = false;
                }
            }

            var recipe = BreakfastRecipe;

            if (BreakfastRecipe == null && LunchRecipe == null && DinnerRecipe == null && DessertRecipe == null && OtherRecipe == null) { 
                IsDataVisible = false;
                IsLabelVisible = true;
            } else
            {
                IsDataVisible = true;
                IsLabelVisible = false;
            }
        }

        private void SetDataAndExceptionForCategory(string category, MyRecipe? data, Exception? exception)
        {

            _ = Enum.TryParse<PlannerCategory>(category, out var parsedCategory);

            switch (parsedCategory)
            {
                case PlannerCategory.Breakfast:
                    BreakfastRecipe = data;
                    OnPropertyChanged(nameof(BreakfastRecipe));
                    break;
                case PlannerCategory.Lunch:
                    LunchRecipe = data;
                    OnPropertyChanged(nameof(LunchRecipe));
                    break;
                case PlannerCategory.Dinner:
                    DinnerRecipe = data;
                    OnPropertyChanged(nameof(DinnerRecipe));
                    break;
                case PlannerCategory.Dessert:
                    DessertRecipe = data;
                    OnPropertyChanged(nameof(DessertRecipe));
                    break;
                case PlannerCategory.Other:
                    OtherRecipe = data;
                    OnPropertyChanged(nameof(OtherRecipe));
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
