using Microsoft.EntityFrameworkCore;
using VDOEasy.Data.Models;
using VDOEasy.Data.Repositories.Interfaces;

namespace VDOEasy.Data.Repositories
{
    public class MemberTypeRepository : IMemberTypeRepository
    {
        private readonly VdoeasyContext _context;

        public MemberTypeRepository(VdoeasyContext context)
        {
            _context = context;
        }
        public async Task<List<MasMemberType>> GetMemberTypes()
        {
            return await _context.MasMemberTypes.ToListAsync();
        }
        public async Task<string> GetMemberTypeById(int id)
        {
            return await _context.MasMemberTypes
                .Where(x => x.Id == id)
                .Select(x => x.Name)
                .FirstOrDefaultAsync();
        }
    }
}
