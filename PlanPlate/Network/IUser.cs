using Firebase.Auth;


namespace PlanPlate.Network
{
    public interface IUser
    {
        Task<User> SignUpAsync(string email, string password, string username);
        Task<User> LogInAsync(string email, string password);
        bool LogOut();
        User IsLoggedIn();
        
    }
}
