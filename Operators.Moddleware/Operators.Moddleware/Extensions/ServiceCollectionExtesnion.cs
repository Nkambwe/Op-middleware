using Operators.Moddleware.Data.Repositories.access;
using Operators.Moddleware.Data.Repositories;
using Operators.Moddleware.Helpers;
using Operators.Moddleware.Services.Access;
using Operators.Moddleware.Services;
using Operators.Moddleware.Services.Settings;
using Operators.Moddleware.Data.Repositories.Settings;

namespace Operators.Moddleware.Extensions {

    public static class ServiceCollectionExtesnion {

        public static void RegisterServices(this IServiceCollection services){ 
             //..register repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPasswordsRepository, PasswordsRepository>();
            services.AddScoped<IParametersRepository, ParametersRepository>();

            services.AddScoped<IThemeRepository, ThemeRepository>();
            services.AddScoped<IUserThemeRepository, UserThemeRepository>();

            //..logging
            services.AddScoped<IServiceLogger, ServiceLogger>();

            //..register services
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IParameterService, ParameterService>();
            services.AddScoped<IThemeService, ThemeService>();
            services.AddScoped<IUserThemeService, UserThemeService>();
        }
    }
}
