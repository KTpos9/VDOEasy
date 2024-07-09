using VDOEasy.Data.Models;

namespace VDOEasy.Data.Repositories.Interfaces
{
    public interface IMovieTypeRepository
    {
        Task<List<MasMoviesType>> GetMovieTypes();
    }
}