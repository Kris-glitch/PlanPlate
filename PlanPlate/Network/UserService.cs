using Firebase.Auth;

namespace PlanPlate.Network
{
    public class UserService(FirebaseAuthClient authClient) : IUser
    {
        protected readonly FirebaseAuthClient _authClient = authClient;

        public User IsLoggedIn()
        {
            return _authClient.User; 
        }

        public async Task<User> LogInAsync(string email, string password)
        {
            var authResult = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
            return authResult.User;
        }

        public bool LogOut()
        {
            _authClient.SignOut();
            return true;
        }

        public async Task<User> SignUpAsync(string email, string password, string username)
        {
            var authResult = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password, username);
            return authResult.User;
        }
    }
}
