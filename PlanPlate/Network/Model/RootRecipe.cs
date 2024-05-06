using System.Text.Json.Serialization;


namespace PlanPlate.Network.Model
{
    public class RootRecipe
    {
        [JsonPropertyName("meals")]
        public List<Recipe>? Recipes { get; set; }
    }
}
