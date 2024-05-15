namespace PlanPlate.Data.Model
{
    public class RecipeGroup : List<MyRecipe>
    {
        public string Category { get; private set; }

        public RecipeGroup(string category, List<MyRecipe> recipes) : base(recipes)
        {
            Category = category;
        }
    }
}
