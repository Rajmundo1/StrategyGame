using Microsoft.EntityFrameworkCore;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Entities.Technologies;
using StrategyGame.MODEL.Enums;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.DAL.Repositories
{
    public class TechnologyRepository : ITechnologyRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IKingdomRepository kingdomRepository;

        public TechnologyRepository(ApplicationDbContext dbContext,
                                    IKingdomRepository kingdomRepository)
        {
            this.dbContext = dbContext;
            this.kingdomRepository = kingdomRepository;
        }

        public async Task<Technology> DevelopTechnologyAsync(Guid technologyId)
        {
            await dbContext.Technologies.ForEachAsync(technology =>
            {
                if (technology.Id.Equals(technologyId))
                    technology.Status = ResearchStatus.Researched;
            });

            return await dbContext.Technologies.SingleAsync(x => x.Id.Equals(technologyId));
        }

        public async Task<IEnumerable<Technology>> GetTechnologiesAsync(Guid kingdomId)
        {
            return (await kingdomRepository.GetKingdomAsync(kingdomId)).Technologies;
        }

        public async Task<Technology> GetTechnologyAsync(Guid technologyId)
        {
            return await dbContext.Technologies.SingleAsync(x => x.Id.Equals(technologyId));
        }
    }
}
