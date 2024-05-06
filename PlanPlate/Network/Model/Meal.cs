

using System.Text.Json.Serialization;

namespace PlanPlate.Network.Model
{
    public class Meal
    {
        [JsonPropertyName("strMeal")]
        public string? StrMeal { get; set; }

        [JsonPropertyName("strMealThumb")]
        public string? StrMealThumb { get; set; }

        [JsonPropertyName("idMeal")]
        public string? IdMeal { get; set; }
    }
}
