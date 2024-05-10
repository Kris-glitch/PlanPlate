using PlanPlate.Data.Model;
using Plugin.Maui.Calendar.Models;

namespace PlanPlate.Network.Model
{
    public interface IMealPlannerService
    {
        Task SaveRecipeToPlannerAsync(string userId, DateTime selectedDate, string category, List<MyRecipe> recipes);
        Task UpdateRecipeInPlannerAsync(string userId, DateTime selectedDate, string category, List<MyRecipe> updatedRecipes);
        Task DeleteRecipeFromPlannerAsync(string userId, DateTime selectedDate, string recipeId, string category);
        Task<List<MyRecipe>?> GetAllRecipesFromPlanner(string userId, DateTime selectedDate, string category);
    }
}
