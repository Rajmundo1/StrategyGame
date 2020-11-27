using Microsoft.EntityFrameworkCore;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Entities.Units;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.DAL.Repositories
{
    public class AttackRepository : IAttackRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUnitRepository unitRepository;
        private readonly IGameRepository gameRepository;
        private readonly ICountyRepository countyRepository;

        public AttackRepository(ApplicationDbContext dbContext,
                                IUnitRepository unitRepository,
                                IGameRepository gameRepository,
                                ICountyRepository countyRepository)
        {
            this.dbContext = dbContext;
            this.unitRepository = unitRepository;
            this.gameRepository = gameRepository;
            this.countyRepository = countyRepository;
        }

        public async Task Attack(Guid attackerCountyId, Guid defenderCountyId, IEnumerable<Unit> units)
        {
            var attackerCounty = await countyRepository.GetCountyAsync(attackerCountyId);
            var game = await gameRepository.GetGameByKingdomIdAsync(attackerCounty.KingdomId);

            var attackId = Guid.NewGuid();
            var newUnitGroupId = Guid.NewGuid();

            await dbContext.Attacks.AddAsync(new Attack
            {
                AttackerId = attackerCountyId,
                DefenderId = defenderCountyId,
                GameId = game.Id,
                TimeStamp = DateTime.Now,
                Id = attackId
            });

            await dbContext.SaveChangesAsync();

            var unitGroup = new UnitGroup
            {
                AttackId = attackId,
                Id = newUnitGroupId,
            };

            await dbContext.UnitGroups.AddAsync(unitGroup);

            await dbContext.SaveChangesAsync();

            foreach (var unit in units)
            {
                await unitRepository.MoveToUnitGroup(unit.Id, newUnitGroupId);
            }


            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Attack>> GetAllAttacks()
        {
            var attacks = await dbContext.Attacks
                .Include(a => a.AttackerUnits)
                .ThenInclude(au => au.Units)
                .ThenInclude(u => u.UnitSpecifics)
                .ThenInclude(usp => usp.UnitLevels)
                .ToListAsync();

            foreach (var attack in attacks)
            {
                attack.Defender = await countyRepository.GetCountyAsync(attack.DefenderId);
                attack.Attacker = await countyRepository.GetCountyAsync(attack.AttackerId);
            }

            return attacks;
        }

        public async Task<IEnumerable<Attack>> GetAttacks(Guid countyId)
        {
            var attacks = await dbContext.Attacks
                .Include(a => a.AttackerUnits)
                .ThenInclude(au => au.Units)
                .ThenInclude(u => u.UnitSpecifics)
                .ThenInclude(usp => usp.UnitLevels)
                .Where(attack => attack.AttackerId.Equals(countyId))
                .ToListAsync();

            foreach(var attack in attacks)
            {
                attack.Defender = await countyRepository.GetCountyAsync(attack.DefenderId);
                attack.Attacker = await countyRepository.GetCountyAsync(attack.AttackerId);
            }

            return attacks;
        }

        public async Task RemoveAttack(Guid attackId)
        {
            var attack = await dbContext.Attacks.SingleAsync(a => a.Id.Equals(attackId));
            dbContext.Attacks.Remove(attack);

            await dbContext.SaveChangesAsync();
        }
    }
}
