
using PlanPlate.Data.Model;
using PlanPlate.Utils;

namespace PlanPlate.Data
{
    public interface ICookbookRepository
    {
        Task SaveCookbookRecipe(MyRecipe recipe, string userId);
        Task<string?> SaveRecipeImageToStorage(string userId, FileResult photo);
        Task DeleteRecipeImageFromStorage(string storagePath);
        Task UpdateCookbookRecipe(MyRecipe updatedRecipe, string recipeId, string userId);
        Task DeleteCookbookRecipe(string recipeId, string userId);
        Task<DataOrException<IEnumerable<MyRecipe>, Exception>> GetAllRecipesFromCookbook(string userId);
        Task<DataOrException<IEnumerable<MyRecipe>, Exception>> GetRecipesByCategoryFromCookbook(string userId, string category);
        Task<DataOrException<IEnumerable<MyRecipe>, Exception>> SearchRecipeFromCookbook(string userId, string name);
        Task<DataOrException<MyRecipe, Exception>> SearchRecipeByIdFromCookbookAsync(string userId, string recipeId);
        Task<DataOrException<IEnumerable<MyCategory>, Exception>> GetAllCategories(string userId);

    }
}
