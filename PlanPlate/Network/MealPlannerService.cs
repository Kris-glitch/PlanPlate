using Firebase.Database;
using Firebase.Database.Query;
using PlanPlate.Data.Model;
using PlanPlate.Network.Model;
using PlanPlate.Utils;

namespace PlanPlate.Network
{
    public class MealPlannerService : IMealPlannerService
    {
        protected readonly FirebaseClient _firebaseClient;
        public MealPlannerService(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }

        public async Task DeleteRecipeFromPlannerAsync(string userId, DateTime selectedDate, string recipeId, string category)
        {
            var date = DateFormater.DateTimeToString(selectedDate);

            var mealPlanerRef = _firebaseClient.Child($"mealPlanner/{userId}/{date}/{category}");
            await mealPlanerRef.Child(recipeId).DeleteAsync();
        }

        public async Task<List<MyRecipe>?> GetAllRecipesFromPlanner(string userId, DateTime selectedDate, string category)
        {
            var date = DateFormater.DateTimeToString(selectedDate);

            var mealPlanerRef = _firebaseClient.Child($"mealPlanner/{userId}/{date}/{category}");

            var recipes = await mealPlanerRef.OnceAsync<MyRecipe>();

            var recipesList = recipes.Select(r =>
            {
                var recipe = r.Object;
                recipe.Id = r.Key;
                return recipe;
            }).ToList();

            return recipesList;
        }

        public async Task SaveRecipeToPlannerAsync(string userId, DateTime selectedDate, string category, List<MyRecipe> recipes)
        {
            var date = DateFormater.DateTimeToString(selectedDate);

            var mealPlanerRef = _firebaseClient.Child($"mealPlanner/{userId}/{date}/{category}");
            await mealPlanerRef.PostAsync(recipes);
        }

        public async Task UpdateRecipeInPlannerAsync(string userId, DateTime selectedDate, string category, List<MyRecipe> updatedRecipes)
        {
            var date = DateFormater.DateTimeToString(selectedDate);

            var mealPlanerRef = _firebaseClient.Child($"mealPlanner/{userId}/{date}");
            await mealPlanerRef.Child(category).PutAsync(updatedRecipes);
        }
    }
}
