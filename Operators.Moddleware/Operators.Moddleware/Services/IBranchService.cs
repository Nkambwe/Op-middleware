using Operators.Moddleware.Data.Entities;

namespace Operators.Moddleware.Services {
    public interface IBranchService {
        Task<Branch> FindBranchByIdAsync(long branchId);
        Task<Branch> FindCodeAsync(string branchCode);
        Task<bool> CreateBranchAsync(Branch branch);
        Task<bool> UpdateBranchAsync(Branch branch);
        Task<bool> DeleteBranchAsync(long branchId);
        Task<bool> BranchExistsAsync(string branchCode);
        Task<bool> BranchExistsByNameAsync(string branchName);
    }
}