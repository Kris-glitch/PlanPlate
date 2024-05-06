using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Extensions.Logging;
using PlanPlate.Data;
using PlanPlate.Network;
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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Outfit-VariableFont.ttf", "OutfitVariableFont");
                    fonts.AddFont("Outfit-Bold.ttf", "OutfitBold");
                    fonts.AddFont("Outfit-Regular.ttf", "OutfitRegular");
                    fonts.AddFont("Font Awesome 6 Brands-Regular-400.ttf", "FAB");
                    fonts.AddFont("Font Awesome 6 Free-Regular-400.ttf", "FAR");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.ttf", "FAS");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = "AIzaSyAJGsoEK8C_gwcye1H98jHT6KcaOZj1p_I",
                AuthDomain = "planplate-89a8f.firebaseapp.com",
                Providers =
                [
                    new EmailProvider()
                ]

            }));
            builder.Services.AddSingleton<IUser, UserService>();
            builder.Services.AddSingleton<IUserRepository, UserRepository>();

            builder.Services.AddSingleton<IHttpFactory, HttpFactoryService>();
            builder.Services.AddSingleton<IRecipeService, RecipeService>();
            builder.Services.AddSingleton<IRecipeRepository, RecipeRepository>();

            builder.Services.AddSingleton<BaseViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<SignupViewModel>();
            builder.Services.AddSingleton<Login>();
            builder.Services.AddSingleton<Signup>();
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<Home>();
            builder.Services.AddTransient<RecipeDetailsViewModel>();
            builder.Services.AddTransient<RecipeDetails>();


            return builder.Build();
        }
    }
}
