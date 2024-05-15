
using PlanPlate.Data.Model;
using PlanPlate.Utils;

namespace PlanPlate.Data
{
    public interface IPlannerRepository
    {
        Task SaveRecipeToPlannerAsync(string userId, DateTime selectedDate, string category, MyRecipe recipe);
        Task DeleteRecipeFromPlannerAsync(string userId, DateTime selectedDate, string category);
        Task<DataOrException<MyRecipe?, Exception>> GetAllRecipesFromPlanner(string userId, DateTime selectedDate, string category);
    }
}
