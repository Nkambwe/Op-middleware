using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {
    public class SettingsRequest {
         [JsonPropertyName("userId")]
         public int UserId { get; set;}
         
         [JsonPropertyName("branchId")]
         public int BranchId { get; set;}
        
         [JsonPropertyName("settingType")]
         public string SettingType { get; set;}

         [JsonPropertyName("attributes")]
         public Attribute[] Attributes { get; set;}

    }
}
