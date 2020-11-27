using Hangfire;
using StrategyGame.BLL.BackgroundJobs.Interfaces;
using StrategyGame.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.BackgroundJobs.Jobs
{
    public class NewRoundJob : INewRoundJob
    {
        private readonly IGameAppService gameAppService;

        public NewRoundJob(IGameAppService gameAppService)
        {
            this.gameAppService = gameAppService;
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task NewRound()
        {
            await gameAppService.NewRound();
        }
    }
}
