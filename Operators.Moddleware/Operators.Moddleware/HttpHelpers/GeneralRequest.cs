using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {

    public class GeneralRequest {

         [JsonPropertyName("userId")]
         public int UserId { get; set;}
         
         [JsonPropertyName("branchId")]
         public int BranchId { get; set;}
    }
}
