namespace PlanPlate.Network
{
    public interface IStorageService
    {
        Task<string?> SaveRecipeImageAsync(string userId, FileResult photo);
        Task DeleteRecipeImageAsync(string storagePath);

    }
}
