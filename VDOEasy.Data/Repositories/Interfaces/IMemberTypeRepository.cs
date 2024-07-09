using VDOEasy.Data.Models;

namespace VDOEasy.Data.Repositories.Interfaces
{
    public interface IMemberTypeRepository
    {
        Task<List<MasMemberType>> GetMemberTypes();
        Task<string> GetMemberTypeById(int id);
    }
}