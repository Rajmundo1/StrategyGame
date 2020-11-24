﻿using Microsoft.EntityFrameworkCore;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.DAL.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext dbContext;

        public GameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            return await dbContext.Game
                .ToListAsync();
        }

        public async Task<Game> GetGameByKingdomIdAsync(Guid kingdomId)
        {
            var user = await dbContext.Users
                .Include(user => user.Kingdom)
                .SingleAsync(user => user.Kingdom.Id.Equals(kingdomId));

            return await dbContext.Game.SingleAsync(game => 
                game.Id.Equals(
                    user.GameId));
        }
    }
}
