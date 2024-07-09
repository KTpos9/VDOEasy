using VDOEasy.Data.Models;

namespace VDOEasy.Data.Repositories.Interfaces
{
    public interface IBranchRepository
    {
        Task<List<MasBranch>> GetBranches();
    }
}