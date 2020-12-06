using StrategyGame.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Interfaces
{
    public interface IGameAppService
    {
        Task<MainPageDto> GetMainPage();
        Task<MainPageDto> GetCountyPage(Guid countyId);
        Task SetWineConsumption(Guid countyId, int amount);
        Task NewRound();
        Task NewCounty(Guid kingdomId, string countyName);
        Task<IEnumerable<CountyDto>> GetCounties(Guid kingdomId);
        Task<IEnumerable<CountyDto>> GetAllCounties();
        Task<IEnumerable<CountyDto>> GetAllFilteredCounties(string userName);
    }
}
