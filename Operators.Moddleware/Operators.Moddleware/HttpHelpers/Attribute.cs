using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {
    public class Attribute {
        [JsonPropertyName("id")]
        public int Id { get; set;}

        [JsonPropertyName("identifier")]
        public string Identifier { get; set;}

        [JsonPropertyName("parameterName")]
        public string ParameterName { get; set;}

        [JsonPropertyName("parameterValue")]
        public object ParameterValue { get; set;}
    }
}
