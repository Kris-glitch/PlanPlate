using PlanPlate.Data.Model;

namespace PlanPlate.Network
{
    public interface ICookbookService
    {
        Task SaveCookbookRecipeAsync(MyRecipe recipe, string userId);
        Task UpdateCookbookRecipeAsync(MyRecipe updatedRecipe, string recipeId, string userId);
        Task DeleteCookbookRecipeAsync(string recipeId, string userId);
        Task<List<MyRecipe>?> GetAllRecipesFromCookbookAsync(string userId);
        Task<List<MyRecipe>?> GetRecipesByCategoryFromCookbookAsync(string userId, string category);
        Task<List<MyRecipe>?> SearchRecipeFromCookbookAsync(string userId, string name);
        Task<MyRecipe?> SearchRecipeByIdFromCookbookAsync(string userId, string recipeId);
        Task<List<string>> GetAllCategories(string userId);
    }
}
