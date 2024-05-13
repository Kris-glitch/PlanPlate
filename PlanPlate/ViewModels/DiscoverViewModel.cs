﻿using CommunityToolkit.Mvvm.Input;
using PlanPlate.Data.Model;
using PlanPlate.Data;
using PlanPlate.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using PlanPlate.View;



namespace PlanPlate.ViewModels
{
    public partial class DiscoverViewModel : BaseViewModel
    {
        private readonly IRecipeRepository _recipeRepository;

        public DiscoverViewModel(IUserRepository userRepository, IRecipeRepository recipeRepository) : base(userRepository)
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
        bool isRefreshing;

        [ObservableProperty]
        string? searchQuery;

        [ObservableProperty]
        DataOrException<IEnumerable<MyCategory>, Exception>? categories;

        [ObservableProperty]
        DataOrException<IEnumerable<MyMeal>, Exception>? meals;

        [RelayCommand]
        async Task Refresh()
        {
            IsRefreshing = true;
            OnPropertyChanged(nameof(IsRefreshing));

            await GetCategories();
            await SearchMealsByCategory("breakfast");

            IsRefreshing = false;
            OnPropertyChanged(nameof(IsRefreshing));
        }

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
        private async Task GoToRecipeDetails(string recipeId)
        {
            if (recipeId != null)
            {
                await Shell.Current.GoToAsync($"{nameof(RecipeDetails)}?recipeId={recipeId}");
            }
        }
    }
}