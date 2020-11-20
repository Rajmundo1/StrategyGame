using StrategyGame.BLL.Dtos;
using StrategyGame.MODEL.Entities.Technologies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Interfaces
{
    public interface ITechnologyAppService
    {
        Task<IEnumerable<TechnologyDto>> GetTechnologies(Guid kingdomId);
        Task<TechnologyDetailDto> GetTechnologyDetail(Guid technologyId);
        Task DevelopTechnology(Guid technologyId);
    }
}
