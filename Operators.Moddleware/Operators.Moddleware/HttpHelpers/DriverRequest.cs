using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {

    public class DriverRequest {

         [JsonPropertyName("userId")]
         public int UserId { get; set;}
        
         [JsonPropertyName("ipAddress")]
         public string IpAddress { get; set;}

         [JsonPropertyName("driverId")]
         public int DriverId { get; set;}
        
        [JsonPropertyName("decrypt")]
        public string[] Decrypt { get; set; }
    }
}
