using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.ValidationDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Interfaces
{
    public interface IAuthAppService
    {
        Task<TokenDto> Login(LoginDto loginDto);
        Task Logout();
        Task<TokenDto> Register(RegisterDto registerDto);
        Task<TokenDto> RenewToken(string refreshToken);
    }
}
