﻿using Firebase.Database;
using Firebase.Database.Query;
using PlanPlate.Data.Model;

namespace PlanPlate.Network
{
    class CookbookService : ICookbookService
    {
        protected readonly FirebaseClient _firebaseClient;
        
        public CookbookService(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
           
        }

        public async Task SaveCookbookRecipeAsync(MyRecipe recipe, string userId)
        {
            var cookbookRef = _firebaseClient.Child("cookbook").Child(userId);
            var result = await cookbookRef.PostAsync(recipe) ?? throw new Exception("Something went wrong. Please try again later");
        }

        public async Task DeleteCookbookRecipeAsync(string recipeId, string userId)
        {
            var userCookbookRef = _firebaseClient.Child($"cookbook/{userId}");
            await userCookbookRef.Child(recipeId).DeleteAsync();
        }

        public async Task UpdateCookbookRecipeAsync(MyRecipe updatedRecipe, string recipeId, string userId)
        {
            var userCookbookRef = _firebaseClient.Child($"cookbook/{userId}");
            await userCookbookRef.Child(recipeId).PutAsync(updatedRecipe);
        }

        public async Task<List<MyRecipe>?> GetAllRecipesFromCookbookAsync(string userId)
        {
            var userCookbookRef = _firebaseClient.Child($"cookbook/{userId}");
            var recipes = await userCookbookRef.OnceAsync<MyRecipe>();

            var recipesList = recipes.Select(r =>
            {
                var recipe = r.Object;
                recipe.Id = r.Key;
                return recipe;
            }).ToList();

            return recipesList;       
        }

        public async Task<List<MyRecipe>?> GetRecipesByCategoryFromCookbookAsync(string userId, string category)
        {
            var userCookbookRef = _firebaseClient.Child($"cookbook/{userId}");
            var recipes = await userCookbookRef.OrderBy("Category").EqualTo(category).OnceAsync<MyRecipe>();

            var recipesList = recipes.Select(r =>
            {
                var recipe = r.Object;
                recipe.Id = r.Key; 
                return recipe;
            }).ToList();

            return recipesList;
         
        }

        public async Task<List<MyRecipe>?> SearchRecipeFromCookbookAsync(string userId, string name)
        {
            var userCookbookRef = _firebaseClient.Child($"cookbook/{userId}");
            var recipes = await userCookbookRef.OrderBy("RecipeName").StartAt(name).EndAt(name + "\uf8ff").OnceAsync<MyRecipe>();

            var recipesList = recipes.Select(r =>
            {
                var recipe = r.Object;
                recipe.Id = r.Key;
                return recipe;
            }).ToList();

            return recipesList;
        }

        public async Task<MyRecipe?> SearchRecipeByIdFromCookbookAsync(string userId, string recipeId)
        {
            var userCookbookRef = _firebaseClient.Child($"cookbook/{userId}");
            var recipeSnapshot = await userCookbookRef.Child(recipeId).OnceSingleAsync<MyRecipe>();

            if (recipeSnapshot != null)
            {
                
                recipeSnapshot.Id = recipeId;
            }

            return recipeSnapshot;
        }

        public async Task<List<string>> GetAllCategories(string userId)
        {
            
            var userCookbookRef = _firebaseClient.Child($"cookbook/{userId}");
            var recipes = await userCookbookRef.OnceAsync<MyRecipe>();
  
            var categoriesSet = new HashSet<string>();

            foreach (var recipe in recipes)
            {
                if (recipe != null)
                {
                    var category = recipe.Object.Category;
                    if (category != null)
                    {
                        categoriesSet.Add(category);
                    } 
                }  
            }

            return categoriesSet.ToList();
        }

    }
}
