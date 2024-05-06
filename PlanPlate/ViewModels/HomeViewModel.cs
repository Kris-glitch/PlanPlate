using CommunityToolkit.Mvvm.Input;
using PlanPlate.Data.Model;
using PlanPlate.Data;
using PlanPlate.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using PlanPlate.View;

namespace PlanPlate.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly IRecipeRepository _recipeRepository;

        public HomeViewModel(IUserRepository userRepository, IRecipeRepository recipeRepository) : base(userRepository)
        {
            _recipeRepository = recipeRepository;
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
        string? searchQuery;

        [ObservableProperty]
        DataOrException<IEnumerable<MyCategory>, Exception>? categories;

        [ObservableProperty]
        DataOrException<IEnumerable<MyMeal>, Exception>? meals;

        [RelayCommand]
        async Task PerformSearch()
        {
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                Meals = new DataOrException<IEnumerable<MyMeal>, Exception>
                {
                    Data = null,
                    Loading = true,
                    Exception = null

                };

                try
                {
                    var response = await _recipeRepository.SearchRecipe(SearchQuery);
                    Meals.Data = response.Data;
                    Meals.Exception = response.Exception;
                }
                finally
                {
                    Meals.Loading = false;
                    OnPropertyChanged(nameof(Meals));
                }
            }
            
        }

        [RelayCommand]
        public async Task GetCategories()
        {
            Categories = new DataOrException<IEnumerable<MyCategory>, Exception>
            {
                Data = null,
                Loading = true,
                Exception = null
            };

            try
            {
                var response = await _recipeRepository.GetCategories();

                Categories.Data = response.Data;
                Categories.Exception = response.Exception;
            }

            finally
            {
                Categories.Loading = false;
                OnPropertyChanged(nameof(Categories));
            }
        }

        [RelayCommand]
        public async Task SearchMealsByCategory(string category)
        {
            Meals = new DataOrException<IEnumerable<MyMeal>, Exception>
            {
                Data = null,
                Loading = true,
                Exception = null
                
            };

            if (Meals != null)
            {
                Meals.Loading = true;

                try
                {
                    var response = await _recipeRepository.SearchByCategory(category);

                    Meals.Data = response.Data;
                    Meals.Exception = response.Exception;
                }
                finally
                {
                    Meals.Loading = false;
                    OnPropertyChanged(nameof(Meals));
                }
            }
        }

        [RelayCommand]
        private void GoToRecipeDetails(string recipeId)
        {
            if (recipeId != null)
            {
                Shell.Current.GoToAsync($"{nameof(RecipeDetails)}?recipeId={recipeId}");
            }
        }
    }
}
