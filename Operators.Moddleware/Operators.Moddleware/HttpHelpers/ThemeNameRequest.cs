using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {

    public class ThemeNameRequest {
         [JsonPropertyName("userId")]
         public int UserId { get; set;}
         
         [JsonPropertyName("theme")]
         public string Theme { get; set;}
    }
}
