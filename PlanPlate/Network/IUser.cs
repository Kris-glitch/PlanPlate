using PlanPlate.Data.Model;
using PlanPlate.Utils;

namespace PlanPlate.Network
{
    public interface IUser
    {
        Task<DataOrException<MyUser, Exception>> SignUpAsync(string email, string password);
        Task<DataOrException<MyUser, Exception>> LogInAsync(string email, string password);
        DataOrException<bool, Exception> LogOut();
        DataOrException<MyUser, Exception> IsLoggedIn();
        
    }
}
