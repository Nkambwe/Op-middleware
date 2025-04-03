using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {

    public class BusinessEmployerDto {
        
        [JsonPropertyName("id")]
        public long Id { get; set; }
        
        [JsonPropertyName("employerName")]
        public string EmployerName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
        
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("primaryContact")]
        public string PrimaryContact { get; set; }

        [JsonPropertyName("secondaryContact")]
        public string SecondaryContact { get; set; }

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
        
        [JsonPropertyName("contact")]
        public long? ContactId { get; set; }

        [JsonPropertyName("contact")]
        public ContactDto Contact { get; set; }
    }

}
