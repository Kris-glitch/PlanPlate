using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Firebase.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using PlanPlate.Data;
using PlanPlate.Network;
using PlanPlate.Network.Model;
using PlanPlate.View;
using PlanPlate.ViewModels;

namespace PlanPlate
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Outfit-VariableFont.ttf", "OutfitVariableFont");
                    fonts.AddFont("Outfit-Bold.ttf", "OutfitBold");
                    fonts.AddFont("Outfit-Regular.ttf", "OutfitRegular");
                    fonts.AddFont("fab.otf", "FAB");
                    fonts.AddFont("far.otf", "FAR");
                    fonts.AddFont("fas.otf", "FAS");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

#if ANDROID
            builder.RegisterFirebase();
#endif

            builder.Services.AddSingleton<FirebaseAuthClient>(sp =>
            {
                return new FirebaseAuthClient(new FirebaseAuthConfig
                {
                    ApiKey = "AIzaSyAJGsoEK8C_gwcye1H98jHT6KcaOZj1p_I",
                    AuthDomain = "planplate-89a8f.firebaseapp.com",
                    Providers = new[] { new EmailProvider() }
                });
            });
            builder.Services.AddSingleton<FirebaseClient>(sg =>
            {
               return new FirebaseClient("https://planplate-89a8f-default-rtdb.europe-west1.firebasedatabase.app/");
            });

            builder.Services.AddSingleton<FirebaseStorage>(sp =>
            {
                return new FirebaseStorage("planplate-89a8f.appspot.com");
            });

            builder.Services.AddSingleton<IUser, UserService>();
            builder.Services.AddSingleton<IUserRepository, UserRepository>();

            builder.Services.AddSingleton<IHttpFactory, HttpFactoryService>();
            builder.Services.AddSingleton<IRecipeService, RecipeService>();
            builder.Services.AddSingleton<IRecipeRepository, RecipeRepository>();

            builder.Services.AddSingleton<ICookbookService, CookbookService>();
            builder.Services.AddSingleton<IStorageService, CookbookService>();
            builder.Services.AddSingleton<ICookbookRepository, CookbookRepository>();
            builder.Services.AddSingleton<IMealPlannerService, MealPlannerService>();
            builder.Services.AddSingleton<IPlannerRepository, PlannerRepository>();

            builder.Services.AddSingleton<BaseViewModel>();

            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<Login>();

            builder.Services.AddSingleton<SignupViewModel>();
            builder.Services.AddSingleton<Signup>();

            builder.Services.AddSingleton<DiscoverViewModel>();
            builder.Services.AddSingleton<Discover>();

            builder.Services.AddTransient<RecipeDetailsViewModel>();
            builder.Services.AddTransient<RecipeDetails>();

            builder.Services.AddSingleton<CookbookViewModel>();
            builder.Services.AddSingleton<Cookbook>();

            builder.Services.AddTransient<AddRecipeViewModel>();
            builder.Services.AddTransient<AddRecipe>();

            builder.Services.AddSingleton<PlanViewModel>();
            builder.Services.AddSingleton<Plan>();

            builder.Services.AddSingleton<SettingsViewModel>();
            builder.Services.AddSingleton<Settings>();


            return builder.Build();
        }

        private static MauiAppBuilder RegisterFirebase(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events =>
            {
#if ANDROID
            events.AddAndroid(android => android.OnCreate((activity, bundle) => {
               
            }));
#endif
            });

            return builder;
        }
    }
}
