using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Helpers;
using System.Linq.Expressions;

namespace Operators.Moddleware.Services.Business {

    public class DriverService : IDriverService {

        private readonly IUnitOfWorkFactory _uowf;
        private readonly ServiceLogger _logger;

        public DriverService(IUnitOfWorkFactory uowf){
             _uowf = uowf;
            _logger = new("app-logs") {
                Channel = $"[DRIVERS {DateTime.Now:yyyyMMMdddHHmmss}]"
            };
        }

        public async Task<IList<Driver>> GetAllAsync(bool includeDeleted, Expression<Func<Driver, bool>> where) {
            _logger.LogToFile("Retrieve all drivers");
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Driver>();
            var drivers = await _repo.GetAllAsync(where,includeDeleted);
            if (drivers != null) {
                _logger.LogToFile($"No records found.", "DRIVERS");
            } else {
                _logger.LogToFile($"RESULT : '{drivers.Count}' records returned", "USERS");
            }

            return drivers;
        }
    }

}
