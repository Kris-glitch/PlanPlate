using PlanPlate.Network;
using PlanPlate.Data.Model;
using PlanPlate.Utils;


namespace PlanPlate.Data
{
    class UserRepository (IUser userApi) : IUserRepository
    {
        protected readonly IUser _userApi = userApi;

        public DataOrException<MyUser, Exception> IsUserLoggedIn()
        {
            DataOrException<MyUser, Exception> result = new();

            try
            {            
                var firebaseUser = _userApi.IsLoggedIn();
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
                result.Exception = ex;
            }

            return result;
            
        }

        public async Task<DataOrException<MyUser, Exception>> LogInUserAsync(string email, string password)
        {
            DataOrException<MyUser, Exception> result = new();

            try
            {
                var authResult = await _userApi.LogInAsync(email, password);

                if (authResult == null)
                {
                    throw new Exception("Log in error");
                } 
                else
                {
                    var user = UserMapper.MapfirebaseUserToMyUser(authResult);
                    result.Data = user;
                } 
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }

        public DataOrException<bool, Exception> LogOutUser()
        {
            DataOrException<bool, Exception> result = new();

            try
            {
                _userApi.LogOut();
                result.Data = true;

            } catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;  
        }

        public async Task<DataOrException<MyUser, Exception>> SignUpUserAsync(string email, string password, string username)
        {
            DataOrException<MyUser, Exception> result = new();

            try
            {
                var authResult = await _userApi.SignUpAsync(email, password, username);
                if (authResult == null)
                {
                    throw new Exception("Sign up error");
                } 
                else
                {
                    var user = UserMapper.MapfirebaseUserToMyUser(authResult);
                    result.Data = user;
                }               
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }
    }
}
