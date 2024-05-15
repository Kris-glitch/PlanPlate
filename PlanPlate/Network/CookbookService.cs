using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using PlanPlate.Data.Model;

namespace PlanPlate.Network
{
    class CookbookService : ICookbookService, IStorageService
    {
        protected readonly FirebaseClient _firebaseClient;

        protected readonly FirebaseStorage _firebaseStorage;

        public CookbookService(FirebaseClient firebaseClient, FirebaseStorage firebaseStorage)
        {
            _firebaseClient = firebaseClient;
            _firebaseStorage = firebaseStorage;
           
        }

        public async Task SaveCookbookRecipeAsync(MyRecipe recipe, string userId)
        {
            var cookbookRef = _firebaseClient.Child("cookbook").Child(userId);
            _ = await cookbookRef.PostAsync(recipe) ?? throw new Exception("Something went wrong. Please try again later");
        }

        public async Task DeleteCookbookRecipeAsync(string recipeId, string userId)
        {           
            var userCookbookRef = _firebaseClient.Child("cookbook").Child(userId);
            await userCookbookRef.Child(recipeId).DeleteAsync();
           
        }

        public async Task UpdateCookbookRecipeAsync(MyRecipe updatedRecipe, string recipeId, string userId)
        {
            var userCookbookRef = _firebaseClient.Child("cookbook").Child(userId);
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
            var recipeList = await GetAllRecipesFromCookbookAsync(userId);

            var result = recipeList?.Where(recipe => recipe?.Name?.Contains(name) == true).ToList();

            return result;

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

        public async Task<string?> SaveRecipeImageAsync(string userId, FileResult photo)
        {            
            var storagePath = $"{userId}/{Guid.NewGuid()}"; 

            await _firebaseStorage.Child(storagePath).PutAsync(await photo.OpenReadAsync());

            var downloadUrl = await _firebaseStorage.Child(storagePath).GetDownloadUrlAsync();

            return downloadUrl;
        }

        public async Task DeleteRecipeImageAsync(string storagePath)
        {
            await _firebaseStorage.Child(storagePath).DeleteAsync();
        }

      
    }
}
