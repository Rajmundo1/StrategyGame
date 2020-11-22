using FluentValidation;
using StrategyGame.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.ValidationDtos
{
    public class LoginDtoValidator: AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(ent => ent.UserName)
                .NotEmpty().WithMessage("Email is required");
            RuleFor(ent => ent.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
