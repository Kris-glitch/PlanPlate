
namespace PlanPlate.Data
{
    public interface ICookbookRepository
    {
        Task SearchRecipe(string searchQuery);
    }
}
