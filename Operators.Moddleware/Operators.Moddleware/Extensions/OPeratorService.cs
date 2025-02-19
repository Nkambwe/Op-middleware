﻿using Operators.Moddleware.Data.Repositories.access;
using Operators.Moddleware.Data.Repositories;
using Operators.Moddleware.Helpers;
using Operators.Moddleware.Services.Access;
using Operators.Moddleware.Services;

namespace Operators.Moddleware.Extensions {

    public static class OPeratorService {

        public static void RegisterServices(this IServiceCollection services){ 
             //..register repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPasswordsRepository, PasswordsRepository>();
            services.AddScoped<IParametersRepository, ParametersRepository>();

            //..logging
            services.AddScoped<IServiceLogger, ServiceLogger>();

            //..register services
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IParameterService, ParameterService>();
        }
    }
}
