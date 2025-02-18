using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {
    public class UserRequest {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("decrypt")]
        public string[] Decrypt { get; set; }
    }
}
