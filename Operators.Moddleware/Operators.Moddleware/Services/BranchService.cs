using Operators.Moddleware.Data.Entities;
using Operators.Moddleware.Data.Repositories;
using Operators.Moddleware.Exceptions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services {

    public class BranchService(IBranchRepository repo) : IBranchService {
        private readonly IBranchRepository _repo = repo;
        private readonly ServiceLogger _logger = new("Operations_log");
        
        public async Task<Branch> FindBranchByIdAsync(long branchId) {
            var branch = await _repo.GetAsync(branchId);
            if(branch != null){ 
                _logger.LogToFile($"BRANCH :: Branch '{branch}' found.", "REPOSITORY");
            } else{
                _logger.LogToFile($"BRANCH :: Branch with ID '{branchId}' not found.", "REPOSITORY");
            }

            return branch;
        }

        public async Task<Branch> FindCodeAsync(string branchCode) {
            var branch = await _repo.GetAsync(b => b.BranchCode == branchCode);
            if(branch != null){ 
                _logger.LogToFile($"BRANCH :: Branch '{branch}' found.", "REPOSITORY");
            } else{
                _logger.LogToFile($"BRANCH :: Branch with branch code '{branchCode}' not found.", "REPOSITORY");
            }

            return branch;
        }
        
        public async Task<bool> CreateBranchAsync(Branch branch) {
            _logger.LogToFile($"Attepting to create new user", "REPOSITORY");
            var result = await _repo.InsertAsync(branch);
            if(result){ 
                _logger.LogToFile($"BRANCH :: {branch} created successfully", "REPOSITORY");
            } else{
                _logger.LogToFile($"BRANCH :: {branch} was not created. An error occurred", "REPOSITORY");
            }
            return result;
        }

        public async Task<bool> DeleteBranchAsync(long branchId) {
            var branch = await _repo.GetAsync(branchId);
            if (branch != null) {
                return await _repo.DeleteAsync(branch);
            } else { 
                throw new NotFoundException($"No branch with branch ID '{branchId}' found"); 
            } 
        }


        public async Task<bool> UpdateBranchAsync(Branch branch) {
            _logger.LogToFile($"Attepting to update branch", "REPOSITORY");
            if (!await _repo.ExistsAsync(b => b.BranchCode == branch.BranchCode)) {
                _logger.LogToFile($"NOTFOUND :: Branch '{branch}' already exists", "REPOSITORY");
                throw new NotFoundException($"No branch with code '{branch.BranchCode}' found"); 
            }

            var result = await _repo.UpdateAsync(branch);
            if(result){ 
                _logger.LogToFile($"BRANCH :: {branch} updated successfully", "REPOSITORY");
            } else{
                _logger.LogToFile($"BRANCH :: {branch} was not updated. An error occurred", "REPOSITORY");
            }
            return result;
        }
        public async Task<bool> BranchExistsAsync(string branchCode) {
            var result = await _repo.ExistsAsync(b => b.BranchCode == branchCode);
            if(result){ 
                _logger.LogToFile($"BRANCH :: Branch with code '{branchCode}' found.", "REPOSITORY");
            } else{
                _logger.LogToFile($"BRANCH :: Branch with code '{branchCode}' not found.", "REPOSITORY");
            }
            return result;
        }

        public async Task<bool> BranchExistsByNameAsync(string branchName) {
            var result = await _repo.ExistsAsync(b => b.BranchName == branchName);
            if(result){ 
                _logger.LogToFile($"BRANCH :: Branch with name '{branchName}' found.", "REPOSITORY");
            } else{
                _logger.LogToFile($"BRANCH :: Branch with name '{branchName}' not found.", "REPOSITORY");
            }
            return result;
        }

    }
}
