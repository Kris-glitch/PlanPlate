using PlanPlate.Data.Model;
using PlanPlate.Network;
using PlanPlate.Utils;

namespace PlanPlate.Data
{
    class CookbookRepository(ICookbookService cookbookService, IStorageService storageService) : ICookbookRepository
    {
        protected readonly ICookbookService _cookbookService = cookbookService;

        protected readonly IStorageService _storageService = storageService;

        public async Task<DataOrException<IEnumerable<MyCategory>, Exception>> GetAllCategories(string userId)
        {
            DataOrException<IEnumerable<MyCategory>, Exception> result = new();

            try
            {
                var categoriesResponse = await _cookbookService.GetAllCategories(userId);
                
                if (categoriesResponse != null)
                {
                    var categories = RecipeMapper.MapCookbookCategoriesToMyCategory(categoriesResponse);
                    result.Data = categories;
                }
                else
                {
                    result.Data = null;
                }
            } catch (Exception ex)
            {
                result.Exception = ex;
                FirebaseCrashlyticsLogger.LogException(ex);
            }

            return result;
        }

        public async Task<DataOrException<IEnumerable<MyRecipe>, Exception>> GetAllRecipesFromCookbook(string userId)
        {
            DataOrException<IEnumerable<MyRecipe>, Exception> result = new();

            try
            {
                var recipesResponse = await _cookbookService.GetAllRecipesFromCookbookAsync(userId);

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
                FirebaseCrashlyticsLogger.LogException(ex);
            }

            return result;

        }

        public async Task<DataOrException<IEnumerable<MyRecipe>, Exception>> GetRecipesByCategoryFromCookbook(string userId, string category)
        {
            DataOrException<IEnumerable<MyRecipe>, Exception> result = new();

            try
            {
                var recipesResponse = await _cookbookService.GetRecipesByCategoryFromCookbookAsync(userId, category);

                if (recipesResponse != null)
                {
                    result.Data = recipesResponse;
                }
                else
                {
                    throw new Exception("No result");
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                FirebaseCrashlyticsLogger.LogException(ex);
            }

            return result;
        }

        public async Task<DataOrException<IEnumerable<MyRecipe>, Exception>> SearchRecipeFromCookbook(string userId, string name)
        {
            DataOrException<IEnumerable<MyRecipe>, Exception> result = new();

            try
            {
                var recipesResponse = await _cookbookService.SearchRecipeFromCookbookAsync(userId, name);

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
                FirebaseCrashlyticsLogger.LogException(ex);
            }

            return result;
        }

        public async Task SaveCookbookRecipe(MyRecipe recipe, string userId)
        {
            try
            {
                await _cookbookService.SaveCookbookRecipeAsync(recipe, userId);
            }
            catch (Exception ex)
            {
                FirebaseCrashlyticsLogger.LogException(ex);
                throw;
            }
        }

        public async Task UpdateCookbookRecipe(MyRecipe updatedRecipe, string recipeId, string userId)
        {
            try
            {
                await _cookbookService.UpdateCookbookRecipeAsync(updatedRecipe, recipeId, userId);
            }
            catch (Exception ex)
            {
                FirebaseCrashlyticsLogger.LogException(ex);
                throw;
            }
        }

        public async Task DeleteCookbookRecipe(string recipeId, string userId)
        {
            try
            {
                await _cookbookService.DeleteCookbookRecipeAsync(recipeId, userId);
            }
            catch (Exception ex)
            {
                FirebaseCrashlyticsLogger.LogException(ex);
                throw;
            }

        }

        public async Task<DataOrException<MyRecipe, Exception>> SearchRecipeByIdFromCookbookAsync(string userId, string recipeId)
        {
            DataOrException<MyRecipe, Exception> result = new();

            try
            {
                var recipesResponse = await _cookbookService.SearchRecipeByIdFromCookbookAsync(userId,recipeId);

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
                FirebaseCrashlyticsLogger.LogException(ex);
            }

            return result;
        }

        public async Task<string?> SaveRecipeImageToStorage(string userId, FileResult photo)
        {
            try
            {
                return await _storageService.SaveRecipeImageAsync(userId, photo);

            } catch (Exception ex)
            {
                FirebaseCrashlyticsLogger.LogException(ex);
                throw;
            }
        }

        public async Task DeleteRecipeImageFromStorage(string storagePath)
        {
            try
            {
                await _storageService.DeleteRecipeImageAsync(storagePath);
            }
            catch (Exception ex)
            {
                FirebaseCrashlyticsLogger.LogException(ex);
                throw;
            }
        }
    }
}
