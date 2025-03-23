using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {

    public class DriverDto{
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        
        [JsonPropertyName("middleName")]
        public string MiddleName { get; set; }
        
        [JsonPropertyName("surname")]
        public string SurName { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
        
        [JsonPropertyName("dateOfBirth")]
        public DateTime? DateOfBirth { get; set; }
        
        [JsonPropertyName("categoryId")]
        public long CategoryId { get; set; }
        
        [JsonPropertyName("categoryName")]
        public string CategoryName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("experience")]
        public int Experience { get; set; }
        
        [JsonPropertyName("residentialDistrictId")]
        public long ResidentialDistrictId { get; set; }
        
        [JsonPropertyName("residentialDistrict")]
        public string ResidentialDistrict { get; set; }

        [JsonPropertyName("DistrictOfOriginId")]
        public long? DistrictOfOriginId { get; set; }

        [JsonPropertyName("districtOfOrigin")]
        public string DistrictOfOrigin { get; set; }
        
        [JsonPropertyName("primaryContact")]
        public string PrimaryContact { get; set; }

        [JsonPropertyName("alternativeContact")]
        public string AlternativeContact { get; set; }

        [JsonPropertyName("whatsapp")]
        public string WhatsApp { get; set; }

        [JsonPropertyName("facebook")]
        public string Facebook { get; set; }
        
        [JsonPropertyName("tweeter")]
        public string Tweeter { get; set; }
        
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
        
        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }
        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }
        
        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }
        
        [JsonPropertyName("lastModifiedOn")]
        public DateTime ModifiedOn { get; set; }
        
        [JsonPropertyName("lastModifiedBy")]
        public string ModifiedBy { get; set; }
        
    }
}
