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

        public async Task<MyRecipe> GetRecipeFromPlanner(string userId, DateTime selectedDate, string category)
        {
            var date = DateFormater.DateTimeToString(selectedDate);

            var mealPlannerRef = _firebaseClient.Child($"mealPlanner/{userId}/{date}/{category}");

            var dataSnapshot = await mealPlannerRef.OnceAsync<MyRecipe>();

            var recipesList = dataSnapshot.Select(r =>
            {
                var recipe = r.Object;
                recipe.Id = r.Key;
                return recipe;
            }).ToList();

            return recipesList[0];
        }


        public async Task SaveRecipeToPlannerAsync(string userId, DateTime selectedDate, string category, MyRecipe recipe)
        {
            var date = DateFormater.DateTimeToString(selectedDate);

            var mealPlanerRef = _firebaseClient.Child($"mealPlanner/{userId}/{date}/{category}");
            await mealPlanerRef.PostAsync(recipe);
        }

    }
}
