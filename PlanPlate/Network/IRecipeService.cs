
using PlanPlate.Network.Model;

namespace PlanPlate.Network
{
    interface IRecipeService
    {
        Task<IEnumerable<Category>?> GetCategories();
        Task<IEnumerable<Meal>?> SearchByCategory(string categoryName);
        Task<Recipe?> GetRecipeDetails(string recipeId);
        Task<IEnumerable<Recipe>?> SearchRecipe(string name);

    }
}
