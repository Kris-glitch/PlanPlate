using PlanPlate.Network.Model;
using PlanPlate.Data.Model;
using System.Text.RegularExpressions;

namespace PlanPlate.Utils
{
    public static class RecipeMapper
    {
        public static IEnumerable<MyCategory> MapApiCategoriesToMyCategories(IEnumerable<Category> categories)
        {
            return categories.Select(category => new MyCategory
            {
                CategoryId = category.IdCategory,
                Name = category.StrCategory
            });
        }

        public static IEnumerable<MyMeal> MapApiMealsToMyMeals(IEnumerable<Meal> meals)
        {
            return meals.Select(meal => new MyMeal
            {
                Name = meal.StrMeal,
                Id = meal.IidMeal,
                Uri = meal.StrMealThumb
            });
        }

        public static IEnumerable<MyRecipe> MapApiRecipesToMyRecipes(IEnumerable<Recipe> recipes)
        {
            return recipes.Select(recipe => new MyRecipe
            {
                Id = recipe.IdMeal,
                Name = recipe.StrMeal,
                Category = recipe.StrCategory,
                Instructions = recipe.StrInstructions,
                Image = recipe.StrMealThumb,
                Tags = recipe.StrTags,
                Ingredients = GetIngredients(recipe)
            });
        }
        public static MyRecipe MapApiRecipeToMyRecipe(Recipe recipe)
        {
            return new MyRecipe
            {
                Id = recipe.IdMeal,
                Name = recipe.StrMeal,
                Category = recipe.StrCategory,
                Instructions = recipe.StrInstructions,
                Image = recipe.StrMealThumb,
                Tags = recipe.StrTags,
                Ingredients = GetIngredients(recipe)
            };
        }
        private static List<Ingredient> GetIngredients(Recipe recipe)
        {
            var ingredients = new List<Ingredient>();

            for (int i = 1; i <= 20; i++)
            {
                var ingredientPropName = $"StrIngredient{i}";
                var measurementPropName = $"StrMeasure{i}";

                var ingredientName = (string?)typeof(Recipe).GetProperty(ingredientPropName)?.GetValue(recipe);
                var measurement = (string?)typeof(Recipe).GetProperty(measurementPropName)?.GetValue(recipe);

                if (string.IsNullOrEmpty(ingredientName) || string.IsNullOrEmpty(measurement))
                {
                    break;
                }

                var (quantity, unit) = ParseMeasurement(measurement);
                ingredients.Add(new Ingredient { Name = ingredientName, Quantity = quantity, Unit = unit });
            }

            return ingredients;
        }

        private static (float, string) ParseMeasurement(string measurement)
        {
            var regex = new Regex(@"(\d+(?:\.\d+)?)\s*(.*)"); // Match the number and anything after it
            var match = regex.Match(measurement);

            if (match.Success)
            {
                // Extract the quantity and unit from the matched groups
                float quantity = float.Parse(match.Groups[1].Value);
                string unit = match.Groups[2].Value.Trim(); // Trim any leading or trailing whitespace from the unit
                return (quantity, unit);
            }

            // If parsing fails or if the measurement doesn't include a quantity,
            // assume quantity is 1 and the entire measurement is the unit
            return (1f, measurement.Trim());
        }


    }
}
