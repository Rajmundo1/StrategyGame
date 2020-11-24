using StrategyGame.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.MODEL.Interfaces
{
    public interface IGameRepository
    {
        Task<Game> GetGameByKingdomIdAsync(Guid kingdomId);
        Task<IEnumerable<Game>> GetAllGames();
    }
}
