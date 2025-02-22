using AutoMapper;
using Operators.Moddleware.Data.Entities;
using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.HttpHelpers;
using System.Globalization;

namespace Operators.Moddleware.Helpers {
    public class MappingProfile : Profile {
        public MappingProfile() {


            CreateMap<Branch, BranchResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(o => o.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(o => (o.BranchCode??string.Empty).Trim()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(o => (o.BranchName ?? string.Empty).Trim()))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(o => o.IsActive))
                .ForMember(dest => dest.Deleted, opt => opt.MapFrom(o => o.IsDeleted))
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(o => (o.CreatedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.AddedOn, opt => opt.MapFrom(o => o.CreatedOn))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(o => (o.LastModifiedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(o => o.LastModifiedOn));

            CreateMap<BranchResponse, Branch>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(o => o.Id))
                .ForMember(dest => dest.BranchCode, opt => opt.MapFrom(o => (o.Code ?? string.Empty).Trim()))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(o => (o.Name ?? string.Empty).Trim()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(o => o.Active))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(o => o.Deleted))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(o => (o.AddedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(o => o.AddedOn))
                .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(o => (o.ModifiedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.LastModifiedOn, opt => opt.MapFrom(o => o.ModifiedOn));

            CreateMap<Role, RoleResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(o => o.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(o => (o.RoleName??string.Empty).Trim()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(o => (o.Description ?? string.Empty).Trim()))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(o => o.IsActive))
                .ForMember(dest => dest.Deleted, opt => opt.MapFrom(o => o.IsDeleted))
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(o => (o.CreatedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.AddedOn, opt => opt.MapFrom(o => o.CreatedOn))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(o => (o.LastModifiedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(o => o.LastModifiedOn));

            CreateMap<RoleResponse, Role>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(o => o.Id))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(o => (o.Name ?? string.Empty).Trim()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(o => (o.Description ?? string.Empty).Trim()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(o => o.Active))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(o => o.Deleted))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(o => (o.AddedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(o => o.AddedOn))
                .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(o => (o.ModifiedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.LastModifiedOn, opt => opt.MapFrom(o => o.ModifiedOn));

            CreateMap<Permission, PermissionResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(o => o.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(o => (o.PermissionName??string.Empty).Trim()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(o => (o.Description ?? string.Empty).Trim()))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(o => o.IsActive))
                .ForMember(dest => dest.Deleted, opt => opt.MapFrom(o => o.IsDeleted))
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(o => (o.CreatedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.AddedOn, opt => opt.MapFrom(o => o.CreatedOn))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(o => (o.LastModifiedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(o => o.LastModifiedOn));

             CreateMap<PermissionResponse, Permission>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(o => o.Id))
                .ForMember(dest => dest.PermissionName, opt => opt.MapFrom(o => (o.Name??string.Empty).Trim()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(o => (o.Description ?? string.Empty).Trim()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(o => o.Active))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(o => o.Deleted))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(o => (o.AddedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(o => o.AddedOn))
                .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(o => (o.ModifiedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.LastModifiedOn, opt => opt.MapFrom(o => o.ModifiedOn));

            CreateMap<User, UserResponse>()
                .ForPath(dest => dest.Data.Id, opt => opt.MapFrom(o => o.Id))
                .ForPath(dest => dest.Data.BranchId, opt => opt.MapFrom(o => o.BranchId))
                .ForPath(dest => dest.Data.BranchCode, opt => opt.MapFrom(o => (o.Branch != null ? o.Branch.BranchCode : string.Empty).Trim()))
                .ForPath(dest => dest.Data.BranchName, opt => opt.MapFrom(o => (o.Branch != null ? o.Branch.BranchName : string.Empty).Trim()))
                .ForPath(dest => dest.Data.RoleId, opt => opt.MapFrom(o => o.RoleId))
                .ForPath(dest => dest.Data.Role, opt => opt.MapFrom(o => (o.Role != null ? o.Role.RoleName : string.Empty).Trim()))
                .ForPath(dest => dest.Data.Username, opt => opt.MapFrom(o => (o.Username ?? string.Empty).Trim()))
                .ForPath(dest => dest.Data.EmployeeNo, opt => opt.MapFrom(o => (o.EmployeeNo ?? string.Empty).Trim()))
                .ForPath(dest => dest.Data.EmployeeName, opt => opt.MapFrom(o => (o.EmployeeName ?? string.Empty).Trim()))
                .ForPath(dest => dest.Data.Email, opt => opt.MapFrom(o => (o.EmailAddress ?? string.Empty).Trim()))
                .ForPath(dest => dest.Data.Active, opt => opt.MapFrom(o => o.IsActive))
                .ForPath(dest => dest.Data.Verified, opt => opt.MapFrom(o => o.IsVerified))
                .ForPath(dest => dest.Data.Loggedin, opt => opt.MapFrom(o => o.IsLoggedin))
                .ForPath(dest => dest.Data.Deleted, opt => opt.MapFrom(o => o.IsDeleted))
                .ForPath(dest => dest.Data.PasswordId, opt => opt.MapFrom(o => o.CurrentPassword))
                .ForPath(dest => dest.Data.AddedBy, opt => opt.MapFrom(o => (o.CreatedBy ?? string.Empty).Trim()))
                .ForPath(dest => dest.Data.AddedOn, opt => opt.MapFrom(o => o.CreatedOn))
                .ForPath(dest => dest.Data.ModifiedBy, opt => opt.MapFrom(o => (o.LastModifiedBy ?? string.Empty).Trim()))
                .ForPath(dest => dest.Data.ModifiedOn, opt => opt.MapFrom(o => o.LastModifiedOn));

            CreateMap<UserResponse, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(o => o.Data.Id))
                .ForPath(dest => dest.Branch.Id, opt => opt.MapFrom(o => o.Data.BranchId))
                .ForPath(dest => dest.Branch.BranchCode, opt => opt.MapFrom(o => (o.Data.BranchCode ?? string.Empty).Trim()))
                .ForPath(dest => dest.Branch.BranchName, opt => opt.MapFrom(o => (o.Data.BranchName ?? string.Empty).Trim()))
                .ForPath(dest => dest.Role.Id, opt => opt.MapFrom(o => o.Data.RoleId))
                .ForPath(dest => dest.Role.RoleName, opt => opt.MapFrom(o => (o.Data.Role ?? string.Empty).Trim()))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(o => (o.Data.Username ?? string.Empty).Trim()))
                .ForMember(dest => dest.EmployeeNo, opt => opt.MapFrom(o => (o.Data.EmployeeNo ?? string.Empty).Trim()))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(o => (o.Data.EmployeeName ?? string.Empty).Trim()))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(o => (o.Data.Email ?? string.Empty).Trim()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(o => o.Data.Active))
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(o => o.Data.Verified))
                .ForMember(dest => dest.IsLoggedin, opt => opt.MapFrom(o => o.Data.Loggedin))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(o => o.Data.Deleted))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(o => (o.Data.AddedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(o => o.Data.AddedOn))
                .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(o => (o.Data.ModifiedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.LastModifiedOn, opt => opt.MapFrom(o => o.Data.ModifiedOn));

            CreateMap<HttpHelpers.Attribute, ConfigurationParameter>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.Parameter, opt => opt.MapFrom(src => src.ParameterName))
                .ForMember(dest => dest.ParameterValue, opt => opt.MapFrom(src => ConvertParameterValue(src.ParameterValue)))
            
                // Set default values for DomainEntity properties
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "System"))
                .ForMember(dest => dest.LastModifiedOn, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())

                // Ignore navigation property
                .ForMember(dest => dest.Branch, opt => opt.Ignore())
                .ForMember(dest => dest.BranchId, opt => opt.Ignore());

            // Reverse mapping if needed
            CreateMap<ConfigurationParameter, HttpHelpers.Attribute>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.ParameterName, opt => opt.MapFrom(src => src.Parameter))
                .ForMember(dest => dest.ParameterValue, opt => opt.MapFrom(src => src.ParameterValue));
        }

        private string ConvertParameterValue(object value) {
            if (value == null)
                return null;

            // Handle different types of values
            return value switch
            {
                DateTime dateTime => dateTime.ToString("O"), // ISO 8601 format
                DateTimeOffset dateTimeOffset => dateTimeOffset.ToString("O"),
                IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture),
                _ => value.ToString()
            };
        }

    }
}
