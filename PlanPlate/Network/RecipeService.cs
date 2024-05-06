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

            var categoriesRoot = JsonSerializer.Deserialize<RootCategoty>(jsonString, options);

            return categoriesRoot?.Categories; 
        }

        public async Task<Recipe?> GetRecipeDetails(string recipeId)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{BaseUrl}/lookup.php?i={recipeId}");

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var recipe = JsonSerializer.Deserialize<RootRecipe>(jsonString, options);
            return recipe?.Recipes?[0];
        }

        public async Task<IEnumerable<Meal>?> SearchByCategory(string categoryName)
        {
            var httpClient = new HttpClient(); // ask Ilija why is this a problem
            HttpResponseMessage? response = await httpClient.GetAsync($"{BaseUrl}/filter.php?c={categoryName}");

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var meals = JsonSerializer.Deserialize<RootMeal>(jsonString, options);
            return meals?.Meals;

        }

        public async Task<IEnumerable<Recipe>?> SearchRecipe(string name)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{BaseUrl}/search.php?s={name}");

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var recipes = JsonSerializer.Deserialize<RootRecipe>(jsonString, options);
            return recipes?.Recipes;

        }
    }
}
