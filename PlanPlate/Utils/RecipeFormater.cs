
using PlanPlate.Data.Model;
using System.Text;

namespace PlanPlate.Utils
{
    public static class RecipeFormater
    {
        public static string FormatRecipe(MyRecipe recipe)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Name: {recipe.Name}");

            sb.AppendLine($" ");

            sb.AppendLine($"Category: {recipe.Category}");

            sb.AppendLine($" ");

            sb.AppendLine("Ingredients:");

            if (recipe.Ingredients != null)
            {
                sb.AppendLine(FormatIngredients(recipe.Ingredients));
            }

            sb.AppendLine($" ");

            sb.AppendLine($"Instructions:\n{recipe.Instructions}");

            sb.AppendLine($" ");

            sb.AppendLine($"Recipe by: {recipe.RecipeBy}");

            return sb.ToString();
        }

        public static string FormatIngredients(List<Ingredient> ingredients)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var ingredient in ingredients)
            {
                sb.AppendLine($"{ingredient.Quantity} {ingredient.Unit} {ingredient.Name}");
            }

            return sb.ToString();
        }


    }
}
