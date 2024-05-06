

using System.Text.Json.Serialization;

namespace PlanPlate.Network.Model
{
    public class Category
    {
        [JsonPropertyName("idCategory")]
        public string? IdCategory { get; set; }

        [JsonPropertyName("strCategory")]
        public string? StrCategory { get; set; }

        [JsonPropertyName("strCategoryThumb")]
        public string? StrCategoryThumb { get; set; }

        [JsonPropertyName("strCategoryDescription")]
        public string? StrCategoryDescription { get; set; }
    }
}
