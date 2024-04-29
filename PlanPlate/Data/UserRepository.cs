using PlanPlate.Network;
using Firebase.Auth;
using PlanPlate.Data.Model;
using PlanPlate.Utils;
using static Google.Apis.Auth.OAuth2.Web.AuthorizationCodeWebApp;



namespace PlanPlate.Data
{
    class UserRepository : IUser
    {

        private readonly FirebaseAuthClient _authClient;

        public UserRepository(FirebaseAuthClient authClient)
        {
            _authClient = authClient;
        }

        public DataOrException<MyUser, Exception> IsLoggedIn()
        {
            DataOrException<MyUser, Exception> result = new();

            try
            {
                var firebaseUser = _authClient.User;
                if (firebaseUser == null)
                {
                    throw new Exception("No logged user");

                }
                else
                {
                    var user = UserMapper.MapfirebaseUserToMyUser(firebaseUser);
                    result.Data = user;
                }
            } catch (Exception ex)
            {
                result.Exception = new Exception("Error while checking login status.", ex);
            }

            return result;
            
        }

        public async Task<DataOrException<MyUser, Exception>> LogInAsync(string email, string password)
        {
            DataOrException<MyUser, Exception> result = new();

            try
            {
                var authResult = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
                var user = UserMapper.MapfirebaseUserToMyUser(authResult.User);
                result.Data = user;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }

        public DataOrException<bool, Exception> LogOut()
        {
            DataOrException<bool, Exception> result = new();

            try
            {
                _authClient.SignOut();
                result.Data = true;

            } catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;  
        }

        public async Task<DataOrException<MyUser, Exception>> SignUpAsync(string email, string password)
        {
            DataOrException<MyUser, Exception> result = new();

            try
            {
                var authResult = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password);
                var user = UserMapper.MapfirebaseUserToMyUser(authResult.User);
                result.Data = user;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }
    }
}
