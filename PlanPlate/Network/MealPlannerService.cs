using Firebase.Database;
using Firebase.Database.Query;
using PlanPlate.Data.Model;
using PlanPlate.Network.Model;
using PlanPlate.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlanPlate.Network
{
    public class MealPlannerService : IMealPlannerService
    {
        protected readonly FirebaseClient _firebaseClient;
        public MealPlannerService(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }

        public async Task DeleteRecipeFromPlannerAsync(string userId, DateTime selectedDate, string category)
        {
            var date = DateFormater.DateTimeToString(selectedDate);

            var mealPlanerRef = _firebaseClient.Child($"mealPlanner/{userId}/{date}/{category}");
            await mealPlanerRef.DeleteAsync();
        }

        public async Task<MyRecipe?> GetRecipeFromPlanner(string userId, DateTime selectedDate, string category)
        {
            var date = DateFormater.DateTimeToString(selectedDate);

            var mealPlannerRef = _firebaseClient.Child($"mealPlanner/{userId}/{date}/{category}");

            var dataSnapshot = await mealPlannerRef.OnceAsync<MyRecipe>();

            if (dataSnapshot != null)
            {
                var recipesList = dataSnapshot.Select(r =>
                {
                    var recipe = r.Object;
                    recipe.Id = r.Key;
                    return recipe;
                }).ToList();

                return recipesList[0];
            }
            else return null;
        }

        public async Task<List<MyRecipe?>> GetWeeklyRecipesFromPlanner(string userId, List<string> week)
        {
           var recipeList = new List<MyRecipe?>();

           foreach( var day in week)
            {
                var mealPlannerRef = _firebaseClient.Child($"mealPlanner/{userId}/{day}");

                var dataSnapshot = await mealPlannerRef.OnceAsync<Dictionary<string, MyRecipe>>();

                if (dataSnapshot != null)
                {
                    foreach(var snapshot in dataSnapshot)
                    {
                        var mealEntries = snapshot.Object;

                        foreach (var entry in mealEntries)
                        {
                            var dayRecipe = entry.Value;
                            
                            recipeList.Add(dayRecipe);
                        }
                    }
                }
            }

            return recipeList;
        }

        public async Task SaveRecipeToPlannerAsync(string userId, DateTime selectedDate, string category, MyRecipe recipe)
        {
            var date = DateFormater.DateTimeToString(selectedDate);

            var mealPlanerRef = _firebaseClient.Child($"mealPlanner/{userId}/{date}/{category}");
            await mealPlanerRef.PostAsync(recipe);
        }



    }
}
