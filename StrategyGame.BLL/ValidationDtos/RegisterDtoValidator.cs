using FluentValidation;
using StrategyGame.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.ValidationDtos
{
    public class RegisterDtoValidator: AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(ent => ent.UserName)
                .NotEmpty().WithMessage("Username is required");
            RuleFor(ent => ent.Password)
                .NotEmpty().WithMessage("Password is required");
            RuleFor(ent => ent.CountyName)
                .NotEmpty().WithMessage("Countyname is required");
        }
    }
}
