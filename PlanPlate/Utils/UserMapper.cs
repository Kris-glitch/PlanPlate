using Firebase.Auth;
using PlanPlate.Data.Model;
using System;
namespace PlanPlate.Utils
{
    public static class UserMapper
    {
        public static MyUser MapfirebaseUserToMyUser(User user)
        {
            MyUser myUser = new()
            {
                Id = user.Uid,
                Email = user.Info.Email,
                Name = user.Info.FirstName
            };
            return myUser;
        }

    }
}
