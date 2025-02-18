using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {

    public class SystemResponse {
        [JsonPropertyName("responseCode")]
        public int ResponseCode { get; set; }
    
        [JsonPropertyName("responseMessage")]
        public string ResponseMessage { get; set; }
    
        [JsonPropertyName("responseDescription")]
        public string ResponseDescription { get; set; }="";
    }

}
