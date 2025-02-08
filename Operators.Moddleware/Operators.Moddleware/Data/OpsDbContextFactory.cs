using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Operators.Moddleware.Helpers;
using System.IO;

namespace Operators.Moddleware.Data {
    public class OpsDbContextFactory : IDesignTimeDbContextFactory<OpsDbContext> {
        public OpsDbContext CreateDbContext(string[] args) {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<OpsDbContext>();
            ServiceLogger _logger = new("Operations_log");
            try {
               
                 //Retrieve the connection string from environment variables
                    string connectionString = Environment.GetEnvironmentVariable("DB_ENV");
                    if (!string.IsNullOrEmpty(connectionString)) {
                        string decryptedString = HashGenerator.DecryptString(connectionString);

                        if(ApplicationUtils.ISLIVE){ 
                            _logger.LogToFile($"CONNECTION URL :: {connectionString}", "INFO");
                        } else {
                            _logger.LogToFile($"CONNECTION URL :: {decryptedString}", "INFO");
                        }

                        optionsBuilder.UseSqlServer(decryptedString);
                    } else { 
                        string msg="Environmental variable name 'DB_ENV' which holds connection string not found";
                        _logger.LogToFile(msg, "DATABASECONNECTION");
                        throw new Exception(msg);
                    }
            } catch (Exception e) {
                _logger.LogToFile($"Database connection failed. {e.Message}", "ERROR");
            }

           

            return new OpsDbContext(optionsBuilder.Options);
        }
    }
}
