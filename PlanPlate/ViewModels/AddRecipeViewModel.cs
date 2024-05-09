using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanPlate.Data;
using PlanPlate.Data.Model;
using System.Collections.ObjectModel;

namespace PlanPlate.ViewModels
{
    public partial class AddRecipeViewModel : BaseViewModel
    {
        private readonly ICookbookRepository _cookbookRepository;
        private readonly Data.IUserRepository _userRepository;
        public ObservableCollection<Ingredient>? IngredientsList { get; set; }

        public AddRecipeViewModel(Data.IUserRepository userRepository, ICookbookRepository cookbookRepository) : base(userRepository)
        {
            _userRepository = userRepository;
            _cookbookRepository = cookbookRepository;
            IngredientsList = [new Ingredient()];
        }

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
        async Task AddImage()
        {
            await CheckPermissions();

        }
        private async Task CheckPermissions()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Photos>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Photos>();
                if (status != PermissionStatus.Granted)
                {
                    OnShowError("Cannot access media library without permission.");
                    return;
                }
            }

            await AccessMediaLibrary();
        }

        private async Task AccessMediaLibrary()
        {
            FileResult? photo = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Select your photo"
            });

            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();

                using (MemoryStream ms = new MemoryStream())
                {
                    await stream.CopyToAsync(ms);
                    byte[] imageBytes = ms.ToArray();


                    string base64Image = Convert.ToBase64String(imageBytes);

                    RecipeImageUri = $"data:image/png;base64,{base64Image}";
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

                var recipeToSave = new MyRecipe
                {
                    Category = RecipeCategory,
                    Name = RecipeName,
                    Image = RecipeImageUri,
                    Ingredients = IngredientsList?.ToList(),
                    Instructions = RecipeInstructions,
                    RecipeBy = RecipeBy
                };

                if (userId != null)
                {
                    await _cookbookRepository.SaveCookbookRecipe(recipeToSave, userId);
                    await AddRecipeViewModel.GoToCookbook();
                }
            }
            catch (Exception ex)
            {
                OnShowError(ex.Message);
            }

        }

        private static async Task GoToCookbook()
        {
            await Shell.Current.GoToAsync("..");
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
