using eCommerce.Core.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Validators
{
    public class RegisterRequestValidator: AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        { 
            // NotNUll, Null, NotEmpty, Empty, MaximumLength(max), MinimumLength(min), Length(min, max), InclusiveBetween(min, max), 
            // ExeclusiveBetween(min, max), Equal(value), NotEqual(value), Matches(regular expression), EmailAddress(), IsInEnum(), Must(condition)
            RuleFor(temp => temp.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address format");

            RuleFor(temp => temp.Password)
                .NotEmpty().WithMessage("Password is required");

            RuleFor(temp => temp.PersonName)
                .NotEmpty().WithMessage("Person name is required");

            RuleFor(temp => temp.Gender)
                .IsInEnum().WithMessage("Gender must be a valid enum value.");
        }
    }
}
