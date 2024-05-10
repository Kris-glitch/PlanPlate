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
        private readonly IUserRepository _userRepository;
        public CookbookViewModel(IUserRepository userRepository, ICookbookRepository cookbookRepository) : base(userRepository)
        {
            _cookbookRepository = cookbookRepository;
            _userRepository = userRepository;
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
        bool isRefreshing;

        [ObservableProperty]
        DataOrException<IEnumerable<MyCategory>, Exception>? categories;

        [ObservableProperty]
        DataOrException<IEnumerable<MyRecipe>, Exception>? recipes;

        [RelayCommand]
        async Task Refresh()
        {
            IsRefreshing = true;
            OnPropertyChanged(nameof(IsRefreshing));

            await GetCategories();
            await GetAllRecipesFromCookbook();

            IsRefreshing = false;
            OnPropertyChanged(nameof(IsRefreshing));
        }

        [RelayCommand]
        private async Task GoToAddRecipe()
        {
            await Shell.Current.GoToAsync($"{nameof(AddRecipe)}");
        }

        [RelayCommand]
        private async Task GoToRecipeDetails(string recipeId)
        {
            if (recipeId != null)
            {
                await Shell.Current.GoToAsync($"{nameof(RecipeDetails)}?recipeId={recipeId}");
            }
        }

        [RelayCommand]
        public async Task GetCategories()
        {
            var userId = GetUserId();
            if (userId == null) return;

            Categories = new DataOrException<IEnumerable<MyCategory>, Exception>
            {
                Data = null,
                Loading = true,
                Exception = null
            };

            try
            {
                var response = await _cookbookRepository.GetAllCategories(userId);

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
        public async Task GetAllRecipesFromCookbook()
        {
            var userId = GetUserId();
            if (userId == null) return;

            Recipes = new DataOrException<IEnumerable<MyRecipe>, Exception>
            {
                Data = null,
                Loading = true,
                Exception = null

            };

            try
            {
                var response = await _cookbookRepository.GetAllRecipesFromCookbook(userId);

                Recipes.Data = response.Data;
                Recipes.Exception = response.Exception;
            }

            finally
            {
                Recipes.Loading = false;
                OnPropertyChanged(nameof(Recipes));
            }
        }

        [RelayCommand]
        public async Task GetRecipesByCategoryFromCookbook(string category)
        {
            var userId = GetUserId();
            if (userId == null) return;

            Recipes = new DataOrException<IEnumerable<MyRecipe>, Exception>
            {
                Data = null,
                Loading = true,
                Exception = null

            };

            try
            {
                var response = await _cookbookRepository.GetRecipesByCategoryFromCookbook(userId, category);

                Recipes.Data = response.Data;
                Recipes.Exception = response.Exception;
            }

            finally
            {
                Recipes.Loading = false;
                OnPropertyChanged(nameof(Recipes));
            }

        }

        [RelayCommand]
        private async Task SearchRecipeFromCookbook()
        {
            var userId = GetUserId();
            if (userId == null) return;

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                Recipes = new DataOrException<IEnumerable<MyRecipe>, Exception>
                {
                    Data = null,
                    Loading = true,
                    Exception = null

                };

                try
                {
                    var response = await _cookbookRepository.SearchRecipeFromCookbook(userId, SearchQuery);

                    Recipes.Data = response.Data;
                    Recipes.Exception = response.Exception;
                }

                finally
                {
                    Recipes.Loading = false;
                    OnPropertyChanged(nameof(Recipes));
                }
            }
        }

        [RelayCommand]
        private async Task DeleteRecipe(string recipeId)
        {
            var userId = GetUserId();
            if (userId == null) return;

            try
            {
                await _cookbookRepository.DeleteCookbookRecipe(recipeId, userId);
            }
            catch (Exception ex) 
            {
                Recipes = new DataOrException<IEnumerable<MyRecipe>, Exception>
                {
                    Data = null,
                    Loading = true,
                    Exception = null

                };
                Recipes.Exception = ex;
            }

            await GetAllRecipesFromCookbook();
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
