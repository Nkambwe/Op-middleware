using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {
    public class GeneralResponse<T> : SystemResponse<T>{
        
         [JsonPropertyName("items")]
         public T[] Items { get; set;}
    }
}
