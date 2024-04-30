using PlanPlate.Data.Model;
using PlanPlate.Utils;

namespace PlanPlate.Data
{
    public interface IRecipeRepository
    {
        Task<DataOrException<IEnumerable<MyCategory>, Exception>> GetCategories();
        Task<DataOrException<IEnumerable<MyMeal>, Exception>> SearchByCategory(string categoryName);
        Task<DataOrException<MyRecipe, Exception>> GetRecipeDetails(string recipeId);
        Task<DataOrException<IEnumerable<MyRecipe>, Exception>> SearchRecipe(string name);
    }
}
