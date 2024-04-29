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
            builder.Services.AddSingleton<IUser>(sp =>
            {
               
                var authClient = sp.GetRequiredService<FirebaseAuthClient>();
                return new UserRepository(authClient);
            });

            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<SignupViewModel>();
            builder.Services.AddSingleton<Login>();
            builder.Services.AddSingleton<Signup>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();



            return builder.Build();
        }
    }
}
