using PlanPlate.Data.Model;
using PlanPlate.Utils;


namespace PlanPlate.Data
{
    public interface IUserRepository
    {
        Task<DataOrException<MyUser, Exception>> SignUpUserAsync(string email, string password, string username);
        Task<DataOrException<MyUser, Exception>> LogInUserAsync(string email, string password);
        DataOrException<bool, Exception> LogOutUser();
        DataOrException<MyUser, Exception> IsUserLoggedIn();
    }
}
