using LiteDB;
using PlanPlate.Data.Model;
using PlanPlate.Network;
using PlanPlate.View;

namespace PlanPlate
{
    public partial class App : Application
    {
        Dictionary<string, object> query;
        private readonly IUser _userRepository;
        public App(IUser userRepository)
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
                Task.Run(async () => await GoToMain()).Wait();
                return;
            }
            Task.Run(async () => await GoToLogin()).Wait();
        }

        protected override void OnResume()
        {
            base.OnResume();
            var isLoggedIn = CheckLoginStatus();
            if (isLoggedIn)
            {
                return;
            }
            Task.Run(async () => await GoToLogin()).Wait();
           
        }

        private bool CheckLoginStatus()
        {
            var response = _userRepository.IsLoggedIn();

            if (response.Exception != null)
            {
                return false;
            }
            else
            {
                var user = response.Data;
                if (user != null)
                {
                    query = new Dictionary<string, object>()
                    {
                        {nameof(MyUser), user}
                    };
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private async Task GoToLogin()
        {
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }

        private async Task GoToMain()
        {
            await Shell.Current.GoToAsync($"//MainPage", query);
        }
    }


    }
