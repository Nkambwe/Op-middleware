namespace Operators.Moddleware.HttpHelpers {
    public class UserResponse : SystemResponse {
        public long Id { get; set; }
        public long BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public long PasswordId { get; set; }
        public string Password { get; set; }
        public int ExpiresIn { get; set; }
        public bool ExpirePasswords { get;set; }
        public bool Active { get; set; }
        public bool Verified { get; set; }
        public bool Deleted { get; set; }
        public bool Loggedin  { get; set; }
        public DateTime AddedOn { get; set; }
        public string AddedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string[] Permissions { get; set; }
    }
}
