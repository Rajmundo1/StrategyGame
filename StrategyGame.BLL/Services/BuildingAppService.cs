using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.Interfaces;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Services
{
    public class BuildingAppService : IBuildingAppService
    {
        private readonly IIdentityService identityService;

        public BuildingAppService(IIdentityService identityService
                                    )
        {
            this.identityService = identityService;
        }

        public Task<IEnumerable<BuildingDto>> GetBuildings()
        {
            throw new NotImplementedException();
        }
    }
}
