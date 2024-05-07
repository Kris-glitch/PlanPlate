using Firebase.Database;

namespace PlanPlate.Network.Model
{
    class RecipeDatabaseService(FirebaseClient firebaseClient) : IRecipesDatabase
    {
        protected readonly FirebaseClient _firebaseClient = firebaseClient;

    }
}
