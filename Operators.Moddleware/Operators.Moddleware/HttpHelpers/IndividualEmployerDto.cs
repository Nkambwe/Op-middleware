using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {

    public class IndividualEmployerDto {

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        
        [JsonPropertyName("middleName")]
        public string MiddleName { get; set; }
        
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
        
        [JsonPropertyName("categoryName")]
        public long TypeId { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("primaryContact")]
        public string PrimaryContact { get; set; }

        [JsonPropertyName("secondaryContact")]
        public string SecondaryContact { get; set; }

        [JsonPropertyName("districtId")]
        public long? DistrictId { get; set; }
        
        [JsonPropertyName("districtName")]
        public string DistrictName { get; set; }
        
        [JsonPropertyName("industryId")]
        public long? IndustryId { get; set; }
        
        [JsonPropertyName("Industry")]
        public string Industry { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

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
