using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {

    public class SystemResponse<T> {
        [JsonPropertyName("responseCode")]
        public int ResponseCode { get; set; }
    
        [JsonPropertyName("responseMessage")]
        public string ResponseMessage { get; set; }
    
        [JsonPropertyName("responseDescription")]
        public string ResponseDescription { get; set; }="";

        [JsonPropertyName("data")]
        public T Data { get; set; }

    }

}
