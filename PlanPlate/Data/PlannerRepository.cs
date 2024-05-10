using PlanPlate.Data.Model;
using PlanPlate.Network.Model;
using PlanPlate.Utils;

namespace PlanPlate.Data
{ 
    class PlannerRepository(IMealPlannerService mealPlannerService) : IPlannerRepository
    {
        IMealPlannerService _mealPlannerService = mealPlannerService;

        public async Task DeleteRecipeFromPlannerAsync(string userId, DateTime selectedDate, string recipeId, string category)
        {
            try
            {
                await _mealPlannerService.DeleteRecipeFromPlannerAsync(userId, selectedDate, recipeId, category);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving recipe: {ex.Message}");
                throw;
            }
        }

        public async Task<DataOrException<IEnumerable<MyRecipe>, Exception>> GetAllRecipesFromPlanner(string userId, DateTime selectedDate, string category)
        {
            DataOrException<IEnumerable<MyRecipe>, Exception> result = new();

            try
            {
                var recipesResponse = await _mealPlannerService.GetAllRecipesFromPlanner(userId, selectedDate, category);

                if (recipesResponse != null)
                {

                    result.Data = recipesResponse;
                }
                else
                {
                    result.Data = null;
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }

        public async Task SaveRecipeToPlannerAsync(string userId, DateTime selectedDate, string category, List<MyRecipe> recipes)
        {
            try
            {
                await _mealPlannerService.SaveRecipeToPlannerAsync(userId, selectedDate, category, recipes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving recipe: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateRecipeInPlannerAsync(string userId, DateTime selectedDate, string category, List<MyRecipe> updatedRecipes)
        {
            try
            {
                await _mealPlannerService.UpdateRecipeInPlannerAsync(userId , selectedDate , category, updatedRecipes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving recipe: {ex.Message}");
                throw;
            }
        }
    }
}
