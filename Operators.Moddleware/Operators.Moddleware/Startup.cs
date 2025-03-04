using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data;
using Operators.Moddleware.Extensions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware {

    public class Startup(IConfiguration configuration) {


        public IConfiguration Configuration { get; } = configuration;

        /// <summary>
        /// Servervice configuration
        /// </summary>
        /// <param name="services">Service Interface</param>
        public void ConfigureServices(IServiceCollection services) { 
            
            //..db connection
            ServiceLogger _logger = new("Operations_log");

            try {
               
                services.AddDbContextFactory<OpsDbContext>(options => {
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

            //..logging
            services.AddScoped<IServiceLogger, ServiceLogger>();

            //register UnitOfWork
            services.RegisterUnitOfWork();

            //register Repositories
            services.RegisterRegpositories();

            //..register services
            services.RegisterServices();

            services.AddRazorPages();
            services.AddControllers().AddXmlDataContractSerializerFormatters();
            services.AddEndpointsApiExplorer();
            services.AddEndpointsApiExplorer();

            //..swagger
            services.AddSwaggerGen(); 

            //..add Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //..register other
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
