using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {
    public class SettingsRequest {
         [JsonPropertyName("userId")]
         public int UserId { get; set;}
         
         [JsonPropertyName("branchId")]
         public string BranchId { get; set;}

         [JsonPropertyName("attributes")]
         public ObjectAttribute[] Attributes { get; set;}

         [JsonPropertyName("settingType")]
         public string SettingType { get; set;}
    }

    public class ObjectAttribute {
        [JsonPropertyName("id")]
        public string Id { get; set;}

        [JsonPropertyName("identifier")]
        public string Identifier { get; set;}

        [JsonPropertyName("parameterName")]
        public string ParameterName { get; set;}

        [JsonPropertyName("parameterValue")]
        public object ParameterValue { get; set;}
    }
}
