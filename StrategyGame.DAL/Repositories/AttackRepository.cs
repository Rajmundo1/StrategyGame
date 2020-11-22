using Microsoft.EntityFrameworkCore;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Entities.Units;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.DAL.Repositories
{
    public class AttackRepository : IAttackRepository
    {
        private readonly ApplicationDbContext dbContext;

        public AttackRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;       
        }

        public async Task Attack(Guid attackerCountyId, Guid defenderCountyId, UnitGroup units)
        {
            var attackId = Guid.NewGuid();

            var unitGroup = new UnitGroup
            {
                AttackId = attackId,
                CountyId = attackerCountyId,
                Id = Guid.NewGuid(),
                Units = units.Units
            };

            await dbContext.UnitGroups.AddAsync(unitGroup);

            await dbContext.Attacks.AddAsync(new Attack
            {
                AttackerCountyId = attackerCountyId,
                DefenderCountyId = defenderCountyId,
                AttackerUnits = units,
                TimeStamp = DateTime.Now,
                Id = attackId
            });
        }

        public async Task<IEnumerable<Attack>> GetAllAttacks()
        {
            return await dbContext.Attacks
                .Include(a => a.Attacker)
                .ThenInclude(c => c.Units)
                .ThenInclude(u => u.Units)
                .ThenInclude(u => u.UnitSpecifics)
                .ThenInclude(usp => usp.UnitLevels)
                .Include(a => a.Attacker)
                .ThenInclude(c => c.Kingdom)
                .ThenInclude(k => k.Technologies)
                .ThenInclude(t => t.Specifics)
                .Include(a => a.Attacker)
                .ThenInclude(c => c.Buildings)
                .ThenInclude(b => b.BuildingSpecifics)
                .ThenInclude(bsp => bsp.BuildingLevels)
                .Include(a => a.Defender)
                .ThenInclude(c => c.Kingdom)
                .ThenInclude(k => k.Technologies)
                .ThenInclude(t => t.Specifics)
                .Include(a => a.Defender)
                .ThenInclude(c => c.Units)
                .ThenInclude(u => u.Units)
                .ThenInclude(u => u.UnitSpecifics)
                .ThenInclude(usp => usp.UnitLevels)
                .Include(a => a.Defender)
                .ThenInclude(c => c.Buildings)
                .ThenInclude(b => b.BuildingSpecifics)
                .ThenInclude(bsp => bsp.BuildingLevels)
                .Include(a => a.AttackerUnits)
                .ThenInclude(au => au.Units)
                .ThenInclude(u => u.UnitSpecifics)
                .ThenInclude(usp => usp.UnitLevels)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attack>> GetAttacks(Guid countyId)
        {
            return await dbContext.Attacks
                .Include(a => a.Attacker)
                .ThenInclude(c => c.Units)
                .ThenInclude(u => u.Units)
                .ThenInclude(u => u.UnitSpecifics)
                .ThenInclude(usp => usp.UnitLevels)
                .Include(a => a.Attacker)
                .ThenInclude(c => c.Kingdom)
                .ThenInclude(k => k.Technologies)
                .ThenInclude(t => t.Specifics)
                .Include(a => a.Attacker)
                .ThenInclude(c => c.Buildings)
                .ThenInclude(b => b.BuildingSpecifics)
                .ThenInclude(bsp => bsp.BuildingLevels)
                .Include(a => a.Defender)
                .ThenInclude(c => c.Kingdom)
                .ThenInclude(k => k.Technologies)
                .ThenInclude(t => t.Specifics)
                .Include(a => a.Defender)
                .ThenInclude(c => c.Units)
                .ThenInclude(u => u.Units)
                .ThenInclude(u => u.UnitSpecifics)
                .ThenInclude(usp => usp.UnitLevels)
                .Include(a => a.Defender)
                .ThenInclude(c => c.Buildings)
                .ThenInclude(b => b.BuildingSpecifics)
                .ThenInclude(bsp => bsp.BuildingLevels)
                .Include(a => a.AttackerUnits)
                .ThenInclude(au => au.Units)
                .ThenInclude(u => u.UnitSpecifics)
                .ThenInclude(usp => usp.UnitLevels)
                .Where(attack => attack.Attacker.Id.Equals(countyId))
                .ToListAsync();
        }
    }
}
