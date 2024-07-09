using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Linq;
using VDOEasy.ApplicationCore.Extensions;
using VDOEasy.ApplicationCore.Models;
using VDOEasy.Data.Models;
using VDOEasy.Data.Repositories.Interfaces;
using VDOEasy.Models;

namespace VDOEasy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemberRepository _memberRepository;
        private readonly IMemberTypeRepository _memberTypeRepository;
        private readonly IMovieTypeRepository _movieTypeRepository;
        private readonly IBranchRepository _branchRepository;

        public HomeController(ILogger<HomeController> logger, IMemberRepository memberRepository, IMemberTypeRepository memberTypeRepository, IMovieTypeRepository movieTypeRepository, IBranchRepository branchRepository)
        {
            _logger = logger;
            _memberRepository = memberRepository;
            _memberTypeRepository = memberTypeRepository;
            _movieTypeRepository = movieTypeRepository;
            _branchRepository = branchRepository;
        }

        public async Task<IActionResult> Index()
        {
            var branch = await _branchRepository.GetBranches();
            
            var model = new HomeViewModel
            {
                Branches = branch.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList(),
                MemberTypes = await _memberTypeRepository.GetMemberTypes(),
                MoviesTypes = await _movieTypeRepository.GetMovieTypes()
            };

            //Randomizer.Seed = new Random(8675309);

            //var members = new Faker<TrnMember>()
            //    .RuleFor(o => o.Firstname, f => f.Name.FirstName())
            //    .RuleFor(o => o.Lastname, f => f.Name.LastName())
            //    .RuleFor(o => o.Birthdate, f => f.Date.Past(50))
            //    .RuleFor(o => o.Address, f => f.Address.FullAddress())
            //    .RuleFor(o => o.IdcardNumber, f => f.Random.Number(100000000, 999999999).ToString())
            //    .RuleFor(o => o.MemberTypeId, f => f.Random.Number(1, 2))
            //    .Generate(10);
            //foreach (var member in members)
            //{
            //    await _memberRepository.AddMember(member);
            //}
            return View(model);
        }
        [HttpPost]
        public async Task<PartialViewResult> GetEditUserModal(int memberId)
        {
            var user = await _memberRepository.GetMemberById(memberId);
            var branch = await _branchRepository.GetBranches();

            var model = new HomeViewModel
            {
                Branches = branch.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList(),
                MemberTypes = await _memberTypeRepository.GetMemberTypes(),
                MoviesTypes = await _movieTypeRepository.GetMovieTypes(),
                Id = memberId,
                FirstName = user.Firstname,
                LastName = user.Lastname,
                Birthdate = user.Birthdate,
                Address = user.Address,
                IdcardNumber = user.IdcardNumber,
                MemberTypeId = user.MemberTypeId.Value,
                UserMovieTypes = user.MovieTypes.Select(m => m.Id).ToList(),
            };
            return PartialView("_EditMemberPartial", model);
        }
        public async Task<IActionResult> PrintReciept(int memberId)
        {
            var member = _memberRepository.GetMemberById(memberId).GetAwaiter().GetResult();
            var model = new PrintRecieptModel
            {
                Id = memberId,
                FirstName = member.Firstname,
                LastName = member.Lastname,
                MemberType = await _memberTypeRepository.GetMemberTypeById(member.MemberTypeId.Value),
                Address = member.Address,
            };
            return View(model);
        }
        public IActionResult DeleteUser(int memberId)
        {
            _memberRepository.DeleteMember(memberId);
            return Ok();
        }
        [HttpPost]
        public async Task<JsonResult> Search(int? memberId, string firstName, string lastName, DateTime? birthdate, DataTableOptionModel option)
        {
            var result = await _memberRepository.GetMembers(option, id: memberId, firstName: firstName, lastName: lastName, birthdate: birthdate);
            return result.ToJsonResult(option);
        }
        [HttpPost]
        public IActionResult AddMember(TrnMember member, int memberType, int[] movieType)
        {
            if (ModelState.IsValid)
            {
                member.MemberTypeId = memberType;
                _memberRepository.AddMember(member, movieType).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }
            
            return PartialView("_AddMemberPartial");
        }
        public async Task<IActionResult> UpdateMember(TrnMember member, int memberType, int[] movieType)
        {
            if (ModelState.IsValid)
            {
                member.MemberTypeId = memberType;
                await _memberRepository.UpdateMember(member);
                return RedirectToAction("Index");
            }

            return PartialView("_EditMemberPartial");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMember(int memberId)
        {
            await _memberRepository.DeleteMember(memberId);
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
