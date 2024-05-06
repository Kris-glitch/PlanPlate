using System.Text.Json.Serialization;

namespace PlanPlate.Network.Model
{
    public class RootMeal
    {
        [JsonPropertyName("meals")]
        public List<Meal>? Meals { get; set; }
    }
}
