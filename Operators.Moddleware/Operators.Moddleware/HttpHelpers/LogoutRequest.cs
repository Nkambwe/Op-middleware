using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {

    public class LogoutRequest {

         [JsonPropertyName("userId")]
         public int UserId { get; set;}
        
         [JsonPropertyName("ipAddress")]
         public string IPAddress { get; set;}
    }
}
