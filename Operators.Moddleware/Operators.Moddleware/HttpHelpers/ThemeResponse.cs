using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {
    public class ThemeResponse<T> : SystemResponse<T> {

        [JsonPropertyName("id")]
        public long Id { get; set;}

        [JsonPropertyName("skin")]
        public string Skin { get; set;}

        [JsonPropertyName("color")]
        public string Color { get; set;}

    }

}
