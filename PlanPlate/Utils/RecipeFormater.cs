
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
                foreach (var ingredient in recipe.Ingredients)
                {
                    sb.AppendLine($"{ingredient.Quantity} {ingredient.Unit} {ingredient.Name}");
                }
            }

            sb.AppendLine($" ");

            sb.AppendLine($"Instructions:\n{recipe.Instructions}");

            sb.AppendLine($" ");

            sb.AppendLine($"Recipe by: {recipe.RecipeBy}");

            return sb.ToString();
        }
    }
}
