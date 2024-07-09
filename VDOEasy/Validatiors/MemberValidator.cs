using FluentValidation;
using VDOEasy.Models;

namespace VDOEasy.Validatiors
{
    public class MemberValidator : AbstractValidator<HomeViewModel>
    {
        public MemberValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Firstname must not exceed 100 characters.");
            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Lastname must not exceed 100 characters.");
            RuleFor(x => x.Birthdate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Birthdate is required.");
            RuleFor(x => x.Address)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(225)
                .WithMessage("Address must not exceed 225 characters.");
            RuleFor(x => x.IdcardNumber)
                .Must((model, idCard) => BeAValidIdcardNumber(model.IdcardNumber))
                    .WithMessage("IdcardNumber is not valid");
        }
        private bool BeAValidIdcardNumber(string idcardNumber)
        {
            ReadOnlySpan<char> idcardNumberSpan = idcardNumber.AsSpan();
            int sum = 0;
            for (int i = 0; i < 13; i++)
            {
                sum = sum + (int.Parse(idcardNumberSpan.Slice(i)) * (13 - i));
            }
            var mod = sum % 11;
            int result = 11 - mod;
            if (result / 10 != 0)
            {
                return int.Parse(idcardNumberSpan.Slice(12)) == result / 10;
            }
            else
            {
                return int.Parse(idcardNumberSpan.Slice(12)) == result;
            }
        }
    }
}
