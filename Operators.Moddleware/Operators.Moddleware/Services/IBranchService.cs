using Operators.Moddleware.Data.Entities;

namespace Operators.Moddleware.Services {
    public interface IBranchService {
        Task<Branch> FindBranchByIdAsync(long branchId, bool includeDeleted);
        Task<Branch> FindCodeAsync(string branchCode, bool includeDeleted);
        Task<bool> CreateBranchAsync(Branch branch);
        Task<bool> UpdateBranchAsync(Branch branch, bool includeDeleted);
        Task<bool> DeleteBranchAsync(long branchId, bool markAsDeleted);
        Task<bool> BranchExistsAsync(string branchCode, bool includeDeleted);
        Task<bool> BranchExistsByNameAsync(string branchName, bool includeDeleted);
    }
}