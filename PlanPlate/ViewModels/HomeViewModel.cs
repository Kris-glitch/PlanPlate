
using CommunityToolkit.Mvvm.Input;
using PlanPlate.Data.Model;
using PlanPlate.Data;
using PlanPlate.Utils;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PlanPlate.ViewModels
{
    public partial class HomeViewModel(IUserRepository userRepository, IRecipeRepository recipeRepository) : BaseViewModel(userRepository)
    {
        private readonly IRecipeRepository _recipeRepository = recipeRepository;

        [ObservableProperty]
        public DataOrException<IEnumerable<MyCategory>, Exception>? categories;

        [ObservableProperty]
        public DataOrException<IEnumerable<MyMeal>, Exception>? meals;
        

        [RelayCommand]
        void PerformSearch()
        {

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
            }

        }

        [RelayCommand]
        public async Task SearchMealsByCategory(string category)
        {
            var Meals = new DataOrException<IEnumerable<MyMeal>, Exception>
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
                }
            }

        }


    }
}
