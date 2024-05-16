using Firebase.Auth;
using Firebase.Database;
using Firebase.Storage;
using System.Net;

namespace PlanPlate.Utils
{
    public static class ExceptionHandler
    {
        public static string HandleExceptionForUI(Exception exception)
        {
            if (exception is HttpRequestException httpRequestException)
            {
                
                if (httpRequestException.StatusCode != null)
                {
                    switch (httpRequestException.StatusCode)
                    {
                        case HttpStatusCode.Forbidden:
                            return "Uh-oh! It seems you have stumbled into a forbidden zone.";
                        case HttpStatusCode.NotFound:
                            return "Oops! Looks like there is a problem with your request. Content was Not Found.";
                        case HttpStatusCode.Unauthorized:
                            return "Oops! Your credentials seem to have gone on a little adventure.You are not authorised.";
                        case HttpStatusCode.InternalServerError:
                            return "Yikes! Our servers are taking a break. Hang tight!";
                        case HttpStatusCode.ServiceUnavailable:
                            return "Yikes! Our servers are taking a break. Hang tight!";
                        default:
                            return $"HTTP Error: {httpRequestException.StatusCode} - {httpRequestException.Message}";
                    }
                }
                return $"HTTP Error: {httpRequestException.Message}";
            }
            else if (exception is FirebaseAuthException authException)
            {
                switch (authException.Reason)
                {
                    case AuthErrorReason.WrongPassword:
                        return "Oops! Your credentials seem to have gone on a little adventure.The password is incorrect. Please try again.";
                    case AuthErrorReason.InvalidEmailAddress:
                        return "Oops! Your credentials seem to have gone on a little adventure. The email address is not valid.";
                    case AuthErrorReason.UserNotFound:
                        return "No account found with this email. Please sign up.";
                    default:
                        return $"Firebase Auth Error: {authException.Message}";
                }
            }
            else if (exception is FirebaseException databaseException)
            {
                return $"Yikes! We have problems with our database.Hang tight while we fix it! Please try again later.";
            }
            else if (exception is FirebaseStorageException storageException)
            {

                return $"Yikes! We have problems with our storage.Hang tight while we fix it! Please try again later.";
            }
            else
            {
                return $"An unexpected error happend. Please try again later.";
            }
        }
    }
}
