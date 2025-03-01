using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {
    public class ThemeResponse : SystemResponse {

        [JsonPropertyName("id")]
        public long Id { get; set;}

        [JsonPropertyName("skin")]
        public string Skin { get; set;}

        [JsonPropertyName("color")]
        public string Color { get; set;}

    }

}
