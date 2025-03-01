using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {

    public class ThemeRequest {

        [JsonPropertyName("userId")]
        public int UserId { get; set;}

    }

}
