using Microsoft.EntityFrameworkCore;
using VDOEasy.Data.Models;
using VDOEasy.Data.Repositories.Interfaces;

namespace VDOEasy.Data.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly VdoeasyContext _context;

        public BranchRepository(VdoeasyContext context)
        {
            _context = context;
        }
        public async Task<List<MasBranch>> GetBranches()
        {
            return await _context.MasBranches.ToListAsync();
        }
    }
}
