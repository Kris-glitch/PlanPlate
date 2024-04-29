using PlanPlate.View;

namespace PlanPlate
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Signup), typeof(Signup));
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}
