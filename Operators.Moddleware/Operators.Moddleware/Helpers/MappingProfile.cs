using AutoMapper;
using Operators.Moddleware.Data.Entities;
using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.HttpHelpers;

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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(o => o.Id))
                .ForPath(dest => dest.BranchCode, opt => opt.MapFrom(o => o.Branch != null ? o.Branch.Id : 0))
                .ForPath(dest => dest.BranchCode, opt => opt.MapFrom(o => (o.Branch != null ? o.Branch.BranchCode : string.Empty).Trim()))
                .ForPath(dest => dest.BranchName, opt => opt.MapFrom(o => (o.Branch != null ? o.Branch.BranchName : string.Empty).Trim()))
                .ForPath(dest => dest.RoleId, opt => opt.MapFrom(o => o.Role != null ? o.Role.Id : 0))
                .ForPath(dest => dest.Role, opt => opt.MapFrom(o => (o.Role != null ? o.Role.RoleName : string.Empty).Trim()))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(o => (o.Username ?? string.Empty).Trim()))
                .ForMember(dest => dest.EmployeeNo, opt => opt.MapFrom(o => (o.EmployeeNo ?? string.Empty).Trim()))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(o => (o.EmployeeName ?? string.Empty).Trim()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(o => (o.EmailAddress ?? string.Empty).Trim()))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(o => o.IsActive))
                .ForMember(dest => dest.Verified, opt => opt.MapFrom(o => o.IsVerified))
                .ForMember(dest => dest.Loggedin, opt => opt.MapFrom(o => o.IsLoggedin))
                .ForMember(dest => dest.Deleted, opt => opt.MapFrom(o => o.IsDeleted))
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(o => (o.CreatedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.AddedOn, opt => opt.MapFrom(o => o.CreatedOn))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(o => (o.LastModifiedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(o => o.LastModifiedOn));

            CreateMap<UserResponse, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(o => o.Id))
                .ForPath(dest => dest.Branch.Id, opt => opt.MapFrom(o => o.BranchId))
                .ForPath(dest => dest.Branch.BranchCode, opt => opt.MapFrom(o => (o.BranchCode ?? string.Empty).Trim()))
                .ForPath(dest => dest.Branch.BranchName, opt => opt.MapFrom(o => (o.BranchName ?? string.Empty).Trim()))
                .ForPath(dest => dest.Role.Id, opt => opt.MapFrom(o => o.RoleId))
                .ForPath(dest => dest.Role.RoleName, opt => opt.MapFrom(o => (o.Role ?? string.Empty).Trim()))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(o => (o.Username ?? string.Empty).Trim()))
                .ForMember(dest => dest.EmployeeNo, opt => opt.MapFrom(o => (o.EmployeeNo ?? string.Empty).Trim()))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(o => (o.EmployeeName ?? string.Empty).Trim()))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(o => (o.Email ?? string.Empty).Trim()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(o => o.Active))
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(o => o.Verified))
                .ForMember(dest => dest.IsLoggedin, opt => opt.MapFrom(o => o.Loggedin))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(o => o.Deleted))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(o => (o.AddedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(o => o.AddedOn))
                .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(o => (o.ModifiedBy ?? string.Empty).Trim()))
                .ForMember(dest => dest.LastModifiedOn, opt => opt.MapFrom(o => o.ModifiedOn));
        }

    }
}
