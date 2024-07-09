using VDOEasy.ApplicationCore.Models;
using VDOEasy.Data.Models;

namespace VDOEasy.Data.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        Task AddMember(TrnMember member, int[] movieTypes);
        Task<List<TrnMember>> GetMembers();
        Task<TrnMember> GetMemberById(int id);
        Task<DataTableResultModel<TrnMember>> GetMembers(DataTableOptionModel dtOption, int? id, string firstName = "", string lastName = "", DateTime? birthdate = null);
        Task UpdateMember(TrnMember member);
        Task DeleteMember(int id);
    }
}