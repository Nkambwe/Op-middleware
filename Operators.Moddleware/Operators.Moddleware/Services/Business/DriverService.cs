using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Data.Repositories;
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
                _logger.LogToFile($"RESULT : '{drivers.Count}' records returned", "DIVERS");
            }

            return drivers;
        }

        public async Task<PagedResult<Driver>> GetAllPagedAsync(int page, int pageSize, bool includeDeleted) {
             _logger.LogToFile("Retrieve all drivers");
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Driver>();;
            var drivers = await _repo.PageAllAsync(page, pageSize, includeDeleted);
            if (drivers != null) {
                _logger.LogToFile($"No records found.", "DRIVERS");
            } else {
                _logger.LogToFile($"RESULT : Page('{page}') and PageSize({pageSize})", "DIVERS");
            }

            return drivers;
        }

        
        public async Task<PagedResult<Driver>> PageAllAsync(int page, int size, bool includeDeleted, Expression<Func<Driver, bool>> where = null) {
             _logger.LogToFile("Retrieve all drivers");
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Driver>();
            var drivers = await _repo.PageAllAsync(page, size, includeDeleted, where);
            if (drivers != null) {
                _logger.LogToFile($"No records found.", "DRIVERS");
            } else {
                _logger.LogToFile($"RESULT : Page('{page}') and PageSize({size})", "DRIVERS");
            }

            return drivers;
        }

        public async Task<PagedResult<Driver>> PageAllAsync(CancellationToken token, int page, int size, Expression<Func<Driver, bool>> where = null, bool includeDeleted = false) {
             _logger.LogToFile("Retrieve all drivers");
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Driver>();
            var drivers = await _repo.PageAllAsync(token, page, size, where, includeDeleted);
            if (drivers != null) {
                _logger.LogToFile($"No records found.", "DRIVERS");
            } else {
                _logger.LogToFile($"RESULT : Page('{page}') and PageSize({size})", "DRIVERS");
            }

            return drivers;
        }
    }

}
