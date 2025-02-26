using System.Text.Json.Serialization;

namespace Operators.Moddleware.HttpHelpers {
    public class UserData  {

        [JsonPropertyName("id")]
        public long Id { get; set; }
    
        [JsonPropertyName("branchId")]
        public long BranchId { get; set; }
    
        [JsonPropertyName("branchCode")]
        public string BranchCode { get; set; }
    
        [JsonPropertyName("branchName")]
        public string BranchName { get; set; }
    
        [JsonPropertyName("roleId")]
        public long RoleId { get; set; }
    
        [JsonPropertyName("role")]
        public string Role { get; set; }
    
        [JsonPropertyName("username")]
        public string Username { get; set; }
    
        [JsonPropertyName("employeeNo")]
        public string EmployeeNo { get; set; }
    
        [JsonPropertyName("employeeName")]
        public string EmployeeName { get; set; }
    
        [JsonPropertyName("email")]
        public string Email { get; set; }
    
        [JsonPropertyName("passwordId")]
        public long PasswordId { get; set; }
    
        [JsonPropertyName("password")]
        public string Password { get; set; }
    
        [JsonPropertyName("expiresIn")]
        public int ExpiresIn { get; set; }
    
        [JsonPropertyName("expirePasswords")]
        public bool ExpirePasswords { get; set; }
    
        [JsonPropertyName("active")]
        public bool Active { get; set; }
    
        [JsonPropertyName("verified")]
        public bool Verified { get; set; }
    
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

        [JsonPropertyName("themeId")]
        public long ThemeId { get; set; }
    
        [JsonPropertyName("themeTexture")]
        public string ThemeTexture { get; set; }
    
        [JsonPropertyName("themeColor")]
        public string ThemeColor { get; set; }
    
        [JsonPropertyName("loggedin")]
        public bool Loggedin { get; set; }
    
        [JsonPropertyName("addedOn")]
        public DateTime AddedOn { get; set; }
    
        [JsonPropertyName("addedBy")]
        public string AddedBy { get; set; }
    
        [JsonPropertyName("modifiedOn")]
        public DateTime? ModifiedOn { get; set; }
    
        [JsonPropertyName("modifiedBy")]
        public string ModifiedBy { get; set; }

        [JsonPropertyName("permissions")]
        public string[] Permissions { get; set; } = [];
    }
}
