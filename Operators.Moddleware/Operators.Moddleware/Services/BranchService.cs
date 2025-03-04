using Newtonsoft.Json;
using Operators.Moddleware.Data.Entities;
using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Exceptions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services {

    public class BranchService(IUnitOfWorkFactory uowf) : IBranchService {

        //..field of UnitOfWorkFactory
        private readonly IUnitOfWorkFactory _uowf = uowf;
        private readonly ServiceLogger _logger = new("Operations_log");
        
        public async Task<Branch> FindBranchByIdAsync(long branchId, bool includeDeleted) {

            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Branch>();
            var branch = await _repo.GetAsync(branchId, includeDeleted);
            if(branch != null){ 
                string json = JsonConvert.SerializeObject(branch);
                _logger.LogToFile($"RESULT : '{json}'", "BRANCHES");
            } else{
                _logger.LogToFile($"RESULT :: Branch with ID '{branchId}' not found.", "BRANCHES");
            }

            return branch;
        }

        public async Task<Branch> FindCodeAsync(string branchCode, bool includeDeleted) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Branch>();
            var branch = await _repo.GetAsync(b => b.BranchCode == branchCode, includeDeleted);
            if(branch != null){ 
                string json = JsonConvert.SerializeObject(branch);
                _logger.LogToFile($"RESULT : '{json}'", "BRANCHES");
            } else{
               _logger.LogToFile($"RESULT :: Branch with Code '{branchCode}' not found.", "BRANCHES");
            }

            return branch;
        }
        
        public async Task<bool> CreateBranchAsync(Branch branch) {
            _logger.LogToFile($"Attepting to create new user", "REPOSITORY");
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Branch>();
            var result = await _repo.InsertAsync(branch);
            if(result){ 
                string json = JsonConvert.SerializeObject(result);
               _logger.LogToFile($"RESULT : '{json}'", "BRANCHES");
            } else{
               _logger.LogToFile($"RESULT :Error while creating branch", "ERROR");
            }
            return result;
        }

        public async Task<bool> DeleteBranchAsync(long branchId, bool markAsDeleted) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Branch>();
            var branch = await _repo.GetAsync(branchId, markAsDeleted);
            if (branch != null) {
                return await _repo.DeleteAsync(branch,markAsDeleted);
            } else { 
                throw new NotFoundException($"No branch with branch ID '{branchId}' found"); 
            } 
        }


        public async Task<bool> UpdateBranchAsync(Branch branch, bool includeDeleted) {
            _logger.LogToFile($"Attepting to update branch", "REPOSITORY");
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Branch>();
            if (!await _repo.ExistsAsync(b => b.BranchCode == branch.BranchCode, includeDeleted)) {
                _logger.LogToFile($"NOTFOUND :: Branch '{branch}' already exists", "BRANCHES");
                throw new NotFoundException($"No branch with code '{branch.BranchCode}' found"); 
            }

            var result = await _repo.UpdateAsync(branch, includeDeleted);
            if(result){ 
                string json = JsonConvert.SerializeObject(result);
                _logger.LogToFile($"RESULT :: {json} updated successfully", "BRANCHES");
            } else{
                _logger.LogToFile($"RESULT :: {branch} was not updated. An error occurred", "BRANCHES");
            }
            return result;
        }
        public async Task<bool> BranchExistsAsync(string branchCode, bool includeDeleted) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Branch>();
            var result = await _repo.ExistsAsync(b => b.BranchCode == branchCode, includeDeleted);
            if(result){ 
                string json = JsonConvert.SerializeObject(result);
                _logger.LogToFile($"RESULT :: '{json}'", "BRANCHES");
            } else{
                _logger.LogToFile($"RESULT :: Branch with code '{branchCode}' not found.", "BRANCHES");
            }
            return result;
        }

        public async Task<bool> BranchExistsByNameAsync(string branchName, bool includeDeleted) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Branch>();
            var result = await _repo.ExistsAsync(b => b.BranchName == branchName, includeDeleted);
            if(result){ 
                string json = JsonConvert.SerializeObject(result);
                _logger.LogToFile($"RESULT ::'{json}'", "BRANCHES");
            } else{
                _logger.LogToFile($"RESULT :: Branch with name '{branchName}' not found.", "BRANCHES");
            }
            return result;
        }

    }
}
