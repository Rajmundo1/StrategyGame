using StrategyGame.MODEL.Entities.Technologies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.MODEL.Interfaces
{
    public interface ITechnologyRepository
    {
        Task<IEnumerable<Technology>> GetTechnologiesAsync(Guid kingdomId);
        Task<Technology> GetTechnologyAsync(Guid technologyId);
        Task<Technology> DevelopTechnologyAsync(Guid technologyId);
    }
}
