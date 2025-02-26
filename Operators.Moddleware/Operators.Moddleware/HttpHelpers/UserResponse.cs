using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {

    public class UserResponse : SystemResponse {
        [JsonPropertyName("data")]
        public UserData Data { get; set; }
    }
}
