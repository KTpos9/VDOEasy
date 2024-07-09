using Microsoft.EntityFrameworkCore;
using VDOEasy.ApplicationCore.Extensions;
using VDOEasy.ApplicationCore.Models;
using VDOEasy.Data.Models;
using VDOEasy.Data.Repositories.Interfaces;

namespace VDOEasy.Data.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly VdoeasyContext _context;

        public MemberRepository(VdoeasyContext context)
        {
            _context = context;
        }
        public async Task<DataTableResultModel<TrnMember>> GetMembers(DataTableOptionModel dtOption, int? id, string firstName = "", string lastName = "", DateTime? birthdate = null)
        {
            return _context.TrnMembers
                .Where(x => x.IsActive == true)
                .WhereIf(id is not null, x => x.Id == id)
                .WhereIf(!string.IsNullOrEmpty(firstName), x => x.Firstname.Contains(firstName))
                .WhereIf(!string.IsNullOrEmpty(lastName), x => x.Lastname.Contains(lastName))
                .WhereIf(birthdate is not null, x => x.Birthdate == birthdate)
                .Select(x => new TrnMember
                {
                    Id = x.Id,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname,
                    Birthdate = x.Birthdate,
                    Address = x.Address,
                })
                .ToDataTableResult(dtOption);
        }
        public async Task AddMember(TrnMember member, int[] movieTypes)
        {
            try
            {
                await _context.Database.ExecuteSqlAsync($""" 
                sp_ins_trnMembers 
                @BranchID={member.BranchId}, 
                @Firstname={member.Firstname}, 
                @Lastname={member.Lastname},
                @Birthdate={member.Birthdate},
                @Address={member.Address},
                @IDCardNumber={member.IdcardNumber},
                @MemberTypeId={member.MemberTypeId}
                """);
                var id = await _context.TrnMembers.MaxAsync(x => x.Id);
                foreach (var movieType in movieTypes)
                {
                    await _context.Database.ExecuteSqlAsync($"""
                    sp_ins_trnMembersMovieType
                    @MovieTypeID={movieType},
                    @MemberID={id}
                    """);
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TrnMember>> GetMembers()
        {
            return await _context.TrnMembers
                .FromSql($"sp_sel_trnMembers")
                .ToListAsync();
        }
        public async Task<TrnMember> GetMemberById(int id)
        {
            //return _context.TrnMembers.FromSql($"sp_sel_trnMembersByID @Id={id}").AsEnumerable().FirstOrDefault();
            return _context.TrnMembers
                .Include(x => x.MovieTypes)
                .FirstOrDefault(x => x.Id == id);
        }
        public async Task UpdateMember(TrnMember member)
        {
            await _context.Database.ExecuteSqlAsync($"""
                sp_upd_trnMembers 
                @MemberID={member.Id},
                @BranchID={member.BranchId}, 
                @Firstname={member.Firstname}, 
                @Lastname={member.Lastname},
                @Birthdate={member.Birthdate},
                @Address={member.Address},
                @IDCardNumber={member.IdcardNumber},
                @MemberTypeID={member.MemberTypeId}
                """);
        }
        public async Task DeleteMember(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlAsync($"""
                sp_upd_trnMembersIsActiveByID
                @ID={id},
                @IsActive=0
                """);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
