using PlanPlate.Data.Model;

namespace PlanPlate.Network.Model
{
    public interface IMealPlannerService
    {
        Task SaveRecipeToPlannerAsync(string userId, DateTime selectedDate, string category, MyRecipe recipe);
        Task DeleteRecipeFromPlannerAsync(string userId, DateTime selectedDate, string category);
        Task<MyRecipe?> GetRecipeFromPlanner(string userId, DateTime selectedDate, string category);
    }
}
