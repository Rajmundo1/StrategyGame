using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.ValidationDtos
{
    public class LoginDtoValidator: AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(ent => ent.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
            RuleFor(ent => ent.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
