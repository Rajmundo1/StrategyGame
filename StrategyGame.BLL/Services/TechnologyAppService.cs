using AutoMapper;
using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.Interfaces;
using StrategyGame.MODEL.Exceptions;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Services
{
    public class TechnologyAppService : ITechnologyAppService
    {
        private readonly ITechnologyRepository technologyRepository;
        private readonly IKingdomRepository kingdomRepository;
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        public TechnologyAppService(ITechnologyRepository technologyRepository,
                                    IKingdomRepository kingdomRepository,
                                    IMapper mapper,
                                    IIdentityService identityService)
        {
            this.technologyRepository = technologyRepository;
            this.kingdomRepository = kingdomRepository;
            this.mapper = mapper;
            this.identityService = identityService;
        }

        public async Task DevelopTechnology(Guid technologyId)
        {
            var technology = await technologyRepository.GetTechnologyAsync(technologyId);
            var kingdom = await kingdomRepository.GetKingdomAsync(technology.KingdomId);

            var currentUser = await identityService.GetCurrentUser();
            if (!(await kingdomRepository.IsOwner(kingdom.Id, currentUser.Id)))
            {
                throw new AppException("You aren't the owner of that kingdom");
            }


            if (technology.Specifics.ResearchPointCost <= kingdom.ResearchPoint)
            {
                await kingdomRepository.SpendResearchPointAsync(kingdom.Id, technology.Specifics.ResearchPointCost);
                await technologyRepository.DevelopTechnologyAsync(technologyId);
            }
            else
            {
                throw new AppException("Insufficient research points");
            }
        }

        public async Task<TechnologyDetailDto> GetTechnologyDetail(Guid technologyId)
        {
            var technology = await technologyRepository.GetTechnologyAsync(technologyId);

            var currentUser = await identityService.GetCurrentUser();
            if (!(await kingdomRepository.IsOwner(technology.KingdomId, currentUser.Id)))
            {
                throw new AppException("You aren't the owner of that kingdom");
            }

            return mapper.Map<TechnologyDetailDto>(technology);
        }

        public async Task<IEnumerable<TechnologyDto>> GetTechnologies(Guid kingdomId)
        {
            var currentUser = await identityService.GetCurrentUser();
            if (!(await kingdomRepository.IsOwner(kingdomId, currentUser.Id)))
            {
                throw new AppException("You aren't the owner of that kingdom");
            }

            var technologies = (await kingdomRepository.GetKingdomAsync(kingdomId)).Technologies;

            var result = new List<TechnologyDto>();

            foreach(var technology in technologies)
            {
                result.Add(mapper.Map<TechnologyDto>(technology));
            }

            return result;
        }

    }
}
