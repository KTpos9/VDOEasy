using Microsoft.AspNetCore.Mvc.Rendering;
using VDOEasy.Data.Models;

namespace VDOEasy.Models
{
    public class HomeViewModel
    {
        public List<SelectListItem> Branches { get; set; }
        public List<MasMemberType> MemberTypes { get; set; }
        public List<MasMoviesType> MoviesTypes { get; set; }
        public MemberModalViewModel MemberModal { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Address { get; set; }
        public string IdcardNumber { get; set; }
        public int MemberTypeId { get; set; }
        public List<int> UserMovieTypes { get; set; }
    }
}
