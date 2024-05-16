
using PlanPlate.Data.Model;
using System.Collections.Generic;

namespace PlanPlate.Utils
{
    public static class ShoppingListMaker
    {

        public static List<Ingredient> GetIngredientsFromRecipes(List<MyRecipe?> recipes)
        {
            List <Ingredient> ingredientsList = new List < Ingredient >();

            foreach (var recipe in recipes)
            {
                if (recipe != null)
                {
                    if (recipe.Ingredients != null)
                    {
                        foreach (var ingredient in recipe.Ingredients)
                        {
                            ingredientsList.Add(ingredient);
                        }
                    }
                }
            }
            return ingredientsList;
        }

        public static List<Ingredient> CreateShoppingList(List<Ingredient> ingredients)
        {
            var shoppingList = new List<Ingredient>();

            
            var groupedIngredients = ingredients
                .GroupBy(ingredient => (ingredient.Name?.ToLower(), ingredient.Unit?.ToLower()))
                .Select(group => new Ingredient
                {
                    Name = group.Key.Item1,
                    Quantity = group.Sum(ingredient => ingredient.Quantity),
                    Unit = group.Key.Item2 
                });

            
            shoppingList.AddRange(groupedIngredients);
            return shoppingList;
        }
    }
}
