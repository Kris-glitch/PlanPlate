
using PlanPlate.Data.Model;
using PlanPlate.Utils;

namespace PlanPlate.Data
{
    public interface IPlannerRepository
    {
        Task SaveRecipeToPlannerAsync(string userId, DateTime selectedDate, string category, List<MyRecipe> recipes);
        Task UpdateRecipeInPlannerAsync(string userId, DateTime selectedDate, string category, List<MyRecipe> updatedRecipes);
        Task DeleteRecipeFromPlannerAsync(string userId, DateTime selectedDate, string recipeId, string category);
        Task<DataOrException<IEnumerable<MyRecipe>, Exception>> GetAllRecipesFromPlanner(string userId, DateTime selectedDate, string category);
    }
}
