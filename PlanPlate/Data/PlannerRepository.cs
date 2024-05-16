using PlanPlate.Data.Model;
using PlanPlate.Network.Model;
using PlanPlate.Utils;

namespace PlanPlate.Data
{
    class PlannerRepository(IMealPlannerService mealPlannerService) : IPlannerRepository
    {
        IMealPlannerService _mealPlannerService = mealPlannerService;

        public async Task DeleteRecipeFromPlannerAsync(string userId, DateTime selectedDate, string category)
        {
            try
            {
                await _mealPlannerService.DeleteRecipeFromPlannerAsync(userId, selectedDate, category);
            }
            catch (Exception ex)
            {
                FirebaseCrashlyticsLogger.LogException(ex);
                throw;
            }
        }

        public async Task<DataOrException<MyRecipe?, Exception>> GetAllRecipesFromPlanner(string userId, DateTime selectedDate, string category)
        {
            DataOrException<MyRecipe?, Exception> result = new();

            try
            {
                var recipesResponse = await _mealPlannerService.GetRecipeFromPlanner(userId, selectedDate, category);

                result.Data = recipesResponse;

            }
            catch (Exception ex)
            {
                result.Exception = ex;
                FirebaseCrashlyticsLogger.LogException(ex);
            }

            return result;
        }

        public async Task SaveRecipeToPlannerAsync(string userId, DateTime selectedDate, string category, MyRecipe recipe)
        {
            try
            {
                await _mealPlannerService.SaveRecipeToPlannerAsync(userId, selectedDate, category, recipe);
            }
            catch (Exception ex)
            {
                FirebaseCrashlyticsLogger.LogException(ex);
                throw;
            }
        }

        public async Task<List<MyRecipe?>> GetWeeklyRecipesFromPlanner(string userId, List<string> week)
        {
            try
            {
                return await _mealPlannerService.GetWeeklyRecipesFromPlanner(userId, week);
            }
            catch (Exception ex)
            {
                FirebaseCrashlyticsLogger.LogException(ex);
                throw;
            }
        }
    }
}
