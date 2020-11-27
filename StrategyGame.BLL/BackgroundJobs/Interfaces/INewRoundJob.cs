using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.BackgroundJobs.Interfaces
{
    public interface INewRoundJob
    {
        Task NewRound();
    }
}
