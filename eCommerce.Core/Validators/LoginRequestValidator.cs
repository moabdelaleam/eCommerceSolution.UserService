using eCommerce.Core.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Validators;

public class LoginRequestValidator: AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        // NotNUll, Null, NotEmpty, Empty, MaximumLength(max), MinimumLength(min), Length(min, max), InclusiveBetween(min, max), 
        // ExeclusiveBetween(min, max), Equal(value), NotEqual(value), Matches(regular expression), EmailAddress(), IsInEnum(), Must(condition)

        // Email
        RuleFor(temp => temp.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address format");

        // Password
        RuleFor(temp => temp.Password)
            .NotEmpty().WithMessage("Paswword is required");
    }
}
