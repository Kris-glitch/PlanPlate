using PlanPlate.Network.Model;
using System.Text.Json;

namespace PlanPlate.Network
{
    public class RecipeService(IHttpFactory httpClientFactory) : IRecipeService
    {
        
        protected readonly IHttpFactory _httpClientFactory = httpClientFactory;
        static readonly string BaseUrl = @"https://www.themealdb.com/api/json/v1/1";

        public async Task<IEnumerable<Category>?> GetCategories()
        {
            using var httpClient = _httpClientFactory.CreateHttpClient();
            var response = await httpClient.GetAsync($"{BaseUrl}/categories.php");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            };

            var myObject = JsonSerializer.Deserialize<RootCategoty>(jsonString, options);

            return myObject?.Categories;

            
        }

        public async Task<Recipe?> GetRecipeDetails(string recipeId)
        {
            using var httpClient = _httpClientFactory.CreateHttpClient();
            var response = await httpClient.GetAsync($"lookup.php?i={recipeId}");

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var recipe = JsonSerializer.Deserialize<Recipe>(jsonString);
            return recipe;
        }

        public async Task<IEnumerable<Meal>?> SearchByCategory(string categoryName)
        {
            using var httpClient = _httpClientFactory.CreateHttpClient();
            var response = await httpClient.GetAsync($"filter.php?c={categoryName}");

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var meals = JsonSerializer.Deserialize<IEnumerable<Meal>>(jsonString);
            return meals;

        }

        public async Task<IEnumerable<Recipe>?> SearchRecipe(string name)
        {
            using var httpClient = _httpClientFactory.CreateHttpClient();
            var response = await httpClient.GetAsync($"search.php?s={name}");

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var recipes = JsonSerializer.Deserialize<IEnumerable<Recipe>>(jsonString);
            return recipes;

        }
    }
}
