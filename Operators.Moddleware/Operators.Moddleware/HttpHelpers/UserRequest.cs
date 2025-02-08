using Newtonsoft.Json;

namespace Operators.Moddleware.HttpHelpers {
    public class UserRequest {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("decrypt")]
        public string[] Decrypt { get; set; }
    }
}
