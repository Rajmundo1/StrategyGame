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
            var attackerCounty = await dbContext.Counties.SingleAsync(county => county.Id.Equals(attackerCountyId));
            var defenderCounty = await dbContext.Counties.SingleAsync(county => county.Id.Equals(defenderCountyId));

            await dbContext.Attacks.AddAsync(new Attack
            {
                Attacker = attackerCounty,
                Defender = defenderCounty,
                AttackerUnits = units,
                TimeStamp = DateTime.Now,
                Id = Guid.NewGuid()
            });
        }

        public async Task<IEnumerable<Attack>> GetAllAttacks()
        {
            return await dbContext.Attacks.ToListAsync();
        }

        public async Task<IEnumerable<Attack>> GetAttacks(Guid countyId)
        {
            return await dbContext.Attacks
                .Where(attack => attack.Attacker.Id.Equals(countyId))
                .ToListAsync();
        }
    }
}
