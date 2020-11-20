using StrategyGame.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Interfaces
{
    public interface IGameAppService
    {
        Task<MainPageDto> GetMainPage(Guid kingdomId);
        Task NewRound();
    }
}
