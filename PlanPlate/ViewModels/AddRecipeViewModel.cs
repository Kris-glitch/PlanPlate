using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanPlate.Data;
using PlanPlate.Data.Model;
using PlanPlate.Utils;
using PlanPlate.View;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Collections.ObjectModel;

namespace PlanPlate.ViewModels
{
    [QueryProperty("Recipe", "Recipe")]
    public partial class AddRecipeViewModel : BaseViewModel
    {
        private readonly ICookbookRepository _cookbookRepository;
        private readonly IUserRepository _userRepository;
        public ObservableCollection<Ingredient>? IngredientsList { get; set; }

        public AddRecipeViewModel(IUserRepository userRepository, ICookbookRepository cookbookRepository) : base(userRepository)
        {
            _userRepository = userRepository;
            _cookbookRepository = cookbookRepository;
            IngredientsList = [new Ingredient()];
            
        }

        private FileResult? PhotoFromGallery { get; set; }
        private bool? IsEdit { get; set; }

        [ObservableProperty]
        MyRecipe? recipe;

        [ObservableProperty]
        string? recipeCategory;

        [ObservableProperty]
        string? recipeName;

        [ObservableProperty]
        string? recipeBy;

        [ObservableProperty]
        string? recipeInstructions;

        [ObservableProperty]
        string? recipeImageUri;

        [RelayCommand]
        async Task BackArrowPressed()
        {
          TaskCompletionSource<bool> userResponse = new TaskCompletionSource<bool>();

            OnShowAlert("Are you sure you want to go back? Any unsaved changes will be lost.", result =>
            {
                userResponse.SetResult(result);
            });

            bool leave = await userResponse.Task;

            if (leave)
            {
                await GoBack();
            }
        }


        [RelayCommand]
        async Task AddImage()
        {
            await CheckPermissions();

        }
        private async Task CheckPermissions()
        {
            try
            {

                Plugin.Permissions.Abstractions.PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync <MediaLibraryPermission>();

                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        OnShowError("Cannot access media library without permission.");
                    }

                    status = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();

                    if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                    {
                        OnShowError("Cannot access media library without permission.");
                        return;
                    }
                }

                await AccessMediaLibrary();
            }
            catch(Exception ex)
            {
                OnShowError(ExceptionHandler.HandleExceptionForUI(ex));
            } 
        }

        private async Task AccessMediaLibrary()
        {
            FileResult? photo = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Select your photo"
            });

            if (photo != null)
            {
                PhotoFromGallery = photo;

                var stream = await photo.OpenReadAsync();

                if (stream != null)
                {
                    

                    RecipeImageUri = await StreamToImage.ConvertStreamToImage(stream);
                    OnPropertyChanged(nameof(RecipeImageUri));
                }

            }
        }

        [RelayCommand]
        private void AddIngredient()
        {
            IngredientsList?.Add(new Ingredient());
        }

        [RelayCommand]
        private void DeleteIngredient(Ingredient ingredient)
        {
            if (IngredientsList?.Count != 1) { IngredientsList?.Remove(ingredient); }

        }

        [RelayCommand]
        async Task SaveRecipe()
        {
            if (!ValidateUser()) return;
            if (!ValidateEntries()) return;

            try
            {
                var user = _userRepository.IsUserLoggedIn().Data;
                var userId = user?.Id;

                string? generatedUri = null;

                if (userId != null)
                {  
                    if (PhotoFromGallery != null)
                    {
                        generatedUri = await _cookbookRepository.SaveRecipeImageToStorage(userId, PhotoFromGallery);
                    }

                    var imageUri = generatedUri ?? RecipeImageUri;
                    var recipeToSave = new MyRecipe
                    {
                        Category = RecipeCategory,
                        Name = RecipeName,
                        Image = imageUri,
                        Ingredients = IngredientsList?.ToList(),
                        Instructions = RecipeInstructions,
                        RecipeBy = RecipeBy
                    };

                    if (IsEdit == true)
                    {
                        await _cookbookRepository.UpdateCookbookRecipe(recipeToSave, Recipe!.Id, userId);
                    } else
                    {
                        await _cookbookRepository.SaveCookbookRecipe(recipeToSave, userId);
                    }
                    
                    await GoBack();

                }
            }
            catch (Exception ex)
            {
                OnShowError(ExceptionHandler.HandleExceptionForUI(ex));
            }

        }

        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        public void CheckIfEditRecipe()
        {
            if (Recipe != null)
            {
                IsEdit = true;

                RecipeName = Recipe.Name;
                RecipeImageUri = Recipe.Image;
                RecipeCategory = Recipe.Category;
                RecipeBy = Recipe.RecipeBy;
                RecipeInstructions = Recipe.Instructions;
                
                if (Recipe.Ingredients != null)
                {
                    IngredientsList?.Clear();

                    foreach (var ingredient in Recipe.Ingredients)
                    {
                        IngredientsList?.Add(ingredient);
                    }
                }
            }
        }

        private bool ValidateUser()
        {
            var user = _userRepository.IsUserLoggedIn().Data;
            if (user == null)
            {
                OnShowError("Something went wrong. Please try again later");
                return false;
            }
            return true;
        }

        private bool ValidateEntries()
        {
            if (RecipeImageUri == null)
            {
                OnShowError("Please add an image");
                return false;
            }

            if (string.IsNullOrEmpty(RecipeBy) || string.IsNullOrEmpty(RecipeName) || string.IsNullOrEmpty(RecipeCategory) || string.IsNullOrEmpty(RecipeInstructions))
            {
                OnShowError("Please fill out all fields");
                return false;
            }

            if (IngredientsList == null || IngredientsList.Any(ingredient => string.IsNullOrEmpty(ingredient.Name)
                || string.IsNullOrEmpty(ingredient.Unit)))
            {
                OnShowError("Please provide all ingredients information");
                return false;
            }

            return true;
        }
        public void SubscribeToAlert(Action<string, Action<bool>> showAlertWithChoice)
        {
            ShowAlert += showAlertWithChoice;
        }

        public void UnsubscribeFromAlert(Action<string, Action<bool>> showAlert)
        {
            ShowAlert -= showAlert;
        }

        public void SubscribeToErrorEvents(Action<string> errorHandler)
        {
            ShowError += errorHandler;
        }

        public void UnsubscribeFromErrorEvents(Action<string> errorHandler)
        {
            ShowError -= errorHandler;
        }

    }

}
