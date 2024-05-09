using PlanPlate.Data;
using PlanPlate.View;

namespace PlanPlate
{
    public partial class App : Application
    {
        private readonly IUserRepository _userRepository;
        public App(IUserRepository userRepository)
        {
            InitializeComponent();
            _userRepository = userRepository;

            MainPage = new AppShell();
        }
        protected override void OnStart()
        {
            base.OnStart();
            var isLoggedIn = CheckLoginStatus();

            if (isLoggedIn)
            {
                Task.Run(GoToMain).Wait();
                return;
            }
            Task.Run(GoToLogin).Wait();
        }

        protected override void OnResume()
        {
            base.OnResume();
           var isLoggedIn = CheckLoginStatus();

            if (isLoggedIn)
            {
                return;
            }
            Task.Run(GoToLogin).Wait();
           
        }

        private bool CheckLoginStatus()
        {
            var response = _userRepository.IsUserLoggedIn();

            if (response.Exception != null) return false;
            
           var user = response.Data;

           if (user != null) return true;

           return false;

        }
        private async Task GoToLogin()
        {
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }

        private async Task GoToMain()
        {
            await Shell.Current.GoToAsync($"//{nameof(Discover)}");
        }
    }


    }
