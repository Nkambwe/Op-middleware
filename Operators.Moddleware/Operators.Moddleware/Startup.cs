﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Operators.Moddleware.Data;
using Operators.Moddleware.Data.Repositories;
using Operators.Moddleware.Data.Repositories.access;
using Operators.Moddleware.Extensions;
using Operators.Moddleware.Helpers;
using Operators.Moddleware.Services;
using Operators.Moddleware.Services.Access;

namespace Operators.Moddleware {
    public class Startup {

        
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        /// <summary>
        /// Servervice configuration
        /// </summary>
        /// <param name="services">Service Interface</param>
        public void ConfigureServices(IServiceCollection services) { 
            services.AddRazorPages();
            services.AddControllers().AddXmlDataContractSerializerFormatters();
            services.AddEndpointsApiExplorer();
            services.AddEndpointsApiExplorer();

            //..swagger
            services.AddSwaggerGen(); 

            //..db connection
            ServiceLogger _logger = new("Operations_log");
            try {
               
                services.AddDbContext<OpsDbContext>(options => {
                    //Retrieve the connection string from environment variables
                    string connectionString = Environment.GetEnvironmentVariable("DB_ENV");
                    if (!string.IsNullOrEmpty(connectionString)) {
                        string decryptedString = HashGenerator.DecryptString(connectionString);

                        if(ApplicationUtils.ISLIVE){ 
                            _logger.LogToFile($"CONNECTION URL :: {connectionString}", "INFO");
                        } else {
                            _logger.LogToFile($"CONNECTION URL :: {decryptedString}", "INFO");
                        }

                        options.UseSqlServer(decryptedString);
                    } else { 
                        string msg="Environmental variable name 'DB_ENV' which holds connection string not found";
                        _logger.LogToFile(msg, "DATABASECONNECTION");
                        throw new Exception(msg);
                    }
                    
                });
            } catch (Exception e) {
                _logger.LogToFile($"Database connection failed. {e.Message}", "ERROR");
            }

            //..inject dependecies
            services.RegisterServices();

            //..add Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        /// <summary>
        /// Configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application Builder</param>
        public void Configure(WebApplication app) {

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

    }
}
