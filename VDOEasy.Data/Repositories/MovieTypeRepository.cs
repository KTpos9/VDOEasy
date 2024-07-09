using Microsoft.EntityFrameworkCore;
using VDOEasy.Data.Models;
using VDOEasy.Data.Repositories.Interfaces;

namespace VDOEasy.Data.Repositories
{
    public class MovieTypeRepository : IMovieTypeRepository
    {
        private readonly VdoeasyContext _context;

        public MovieTypeRepository(VdoeasyContext context)
        {
            _context = context;
        }
        public async Task<List<MasMoviesType>> GetMovieTypes()
        {
            return await _context.MasMoviesTypes.ToListAsync();
        }
    }
}
