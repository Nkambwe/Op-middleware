using Operators.Moddleware.Data.Repositories;
using Operators.Moddleware.Services.Access;
using Operators.Moddleware.Services;
using Operators.Moddleware.Services.Settings;
using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Services.Business;

namespace Operators.Moddleware.Extensions {

    public static class ServiceCollectionExtesnion {

        public static void RegisterServices(this IServiceCollection services){ 
            
            //..register services
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IParameterService, ParameterService>();
            services.AddScoped<IThemeService, ThemeService>();
            services.AddScoped<IUserThemeService, UserThemeService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IAttachmentTypeService, AttachmentTypeService>();
            services.AddScoped<IBusinessContactService, BusinessContactService>();
            services.AddScoped<IBusinessEmployerService, BusinessEmployerService>();
            services.AddScoped<IBusinessIndustryService, BusinessIndustryService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IDriverTypeService, DriverTypeService>();
            services.AddScoped<IEmployerTypeService, EmployerTypeService>();
            services.AddScoped<IIndividualEmployerService, IndividualEmployerService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IReferenceService, ReferenceService>();
            
        }

        /// <summary>
        /// Register Repositories to the service middleware
        /// </summary>
        public static void RegisterRegpositories(this IServiceCollection services){ 
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPermissionRepository, PermissionRepository>();
        }

        /// <summary>
        /// Register UnitOfWork to the service middleware
        /// </summary>
        public static void RegisterUnitOfWork(this IServiceCollection services){ 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();

        }
    }
}
