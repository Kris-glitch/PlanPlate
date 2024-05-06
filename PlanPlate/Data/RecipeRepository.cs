using PlanPlate.Data.Model;
using PlanPlate.Network;
using PlanPlate.Utils;


namespace PlanPlate.Data
{
    internal class RecipeRepository(IRecipeService recipeService) : IRecipeRepository
    {
        protected readonly IRecipeService _recipeService = recipeService;

        public async Task<DataOrException<IEnumerable<MyCategory>, Exception>> GetCategories()
        {
            DataOrException<IEnumerable<MyCategory>, Exception> result = new();

            try
            {
                var categoriesResponse = await _recipeService.GetCategories();
                if (categoriesResponse == null)
                {
                    throw new Exception("Something went wrong. Categories are not avaliable");
                }
                else
                {
                    var categories = RecipeMapper.MapApiCategoriesToMyCategories(categoriesResponse);
                    result.Data = categories;
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }

        public async Task<DataOrException<MyRecipe, Exception>> GetRecipeDetails(string recipeId)
        {
            DataOrException<MyRecipe, Exception> result = new();

            try
            {
                var recipeResponse = await _recipeService.GetRecipeDetails(recipeId);
                if (recipeResponse == null)
                {
                    throw new Exception("Something went wrong.Recipe is not avaliable");
                }
                else
                {
                    var recipe = RecipeMapper.MapApiRecipeToMyRecipe(recipeResponse);
                    result.Data = recipe;
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public async Task<DataOrException<IEnumerable<MyMeal>, Exception>> SearchByCategory(string categoryName)
        {
            DataOrException<IEnumerable<MyMeal>, Exception> result = new();

            try
            {
                var mealsResponse = await _recipeService.SearchByCategory(categoryName);
                if (mealsResponse == null)
                {
                    throw new Exception("Something went wrong.Recipes are not avaliable");
                }
                else
                {
                    var meals = RecipeMapper.MapApiMealsToMyMeals(mealsResponse);
                    result.Data = meals;
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public async Task<DataOrException<IEnumerable<MyMeal>, Exception>> SearchRecipe(string name)
        {
            DataOrException<IEnumerable<MyMeal>, Exception> result = new();

            try
            {
                var recipesResponse = await _recipeService.SearchRecipe(name);
                if (recipesResponse == null)
                {
                    throw new Exception("No result");
                }
                else
                {
                    var recipes = RecipeMapper.MapApiRecipesToMyMeals(recipesResponse);
                    result.Data = recipes;
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }
    }

}
