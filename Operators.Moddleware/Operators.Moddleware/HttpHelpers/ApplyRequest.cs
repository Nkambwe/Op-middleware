using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {

    public class ApplyRequest {
        
        [JsonPropertyName("userId")]
        public int UserId { get; set;}
        
        [JsonPropertyName("ipAddress")]
        public string IPAddress { get; set;}

        [JsonPropertyName("decrypt")]
        public string[] Decrypt { get; set; }

        [JsonPropertyName("page")]
        public int Page {get;set;}
        
        [JsonPropertyName("size")]
        public int PageSize {get;set;}

        [JsonPropertyName("includeDeleted")]
        public bool IncludeDeleted {get; set;}
    }
}
