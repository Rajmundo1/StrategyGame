using Microsoft.EntityFrameworkCore;
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
    public class UnitRepository : IUnitRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UnitRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task HireUnitsAsync(int count, Guid countyId, Guid unitSpecificsId)
        {
            var county = await dbContext.Counties
                .Include(c => c.Units)
                .SingleAsync(x => x.Id.Equals(countyId));

            await dbContext.Units.ForEachAsync(u =>
            {
                if(u.UnitSpecificsId.Equals(unitSpecificsId) && u.Level == 1 && u.UnitGroupId.Equals(county.Units.Id))
                {
                    u.Count += count;
                }
            });

            await dbContext.SaveChangesAsync();
        }

        public async Task DevelopUnitsAsync(int count, Guid countyId, Guid unitSpecificsId, int currentLvl)
        {
            var county = await dbContext.Counties
                .Include(c => c.Units)
                .SingleAsync(x => x.Id.Equals(countyId));

            await dbContext.Units.ForEachAsync(u =>
            {
                if (u.UnitSpecificsId.Equals(unitSpecificsId) && u.Level == currentLvl && u.UnitGroupId.Equals(county.Units.Id))
                {
                    u.Count -= count;
                }

                if (u.UnitSpecificsId.Equals(unitSpecificsId) && u.Level == currentLvl + 1 && u.UnitGroupId.Equals(county.Units.Id))
                {
                    u.Count += count;
                }
            });

            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Unit>> GetUnitsAsync(Guid countyId)
        {
            return await Task.Run(async () =>
            {
                var unitGroup =  await dbContext.UnitGroups
                    .Include(ug => ug.Units)
                    .ThenInclude(u => u.UnitSpecifics)
                    .ThenInclude(usp => usp.UnitLevels)
                    .SingleAsync(unitGroup => unitGroup.CountyId.Equals(countyId));

                return unitGroup.Units;
            });
        }

        public async Task RemoveUnitsAsync(int count, Guid countyId, Guid unitSpecificsId, int lvl)
        {
            var county = await dbContext.Counties
                .Include(c => c.Units)
                .SingleAsync(x => x.Id.Equals(countyId));

            await dbContext.Units.ForEachAsync(u =>
            {
                if (u.UnitSpecificsId.Equals(unitSpecificsId) && u.Level == lvl && u.UnitGroupId.Equals(county.Units.Id))
                {
                    u.Count -= count;
                }
            });

            await dbContext.SaveChangesAsync();
        }

        public async Task<UnitSpecifics> GetUnitSpecificsAsync(Guid unitSpecificsId)
        {
            return await dbContext.UnitSpecifics
                .Include(usp => usp.UnitLevels)
                .SingleAsync(x => x.Id.Equals(unitSpecificsId));
        }

        public async Task RemoveUnitByIdAsync(Guid unitId)
        {
            var unitToRemove = await dbContext.Units.SingleAsync(x => x.Id.Equals(unitId));
            unitToRemove.Count -= 1;

            await dbContext.SaveChangesAsync();
        }

        public async Task MoveToUnitGroup(Guid unitspecificsId, int lvl, int count, Guid unitGroupId)
        {
            await dbContext.UnitGroups
                .Include(ug => ug.Units)
                .ForEachAsync(u =>
                {
                    if (u.Id.Equals(unitGroupId))
                    {
                        foreach(var units in u.Units)
                        {
                            if(units.Level == lvl && units.UnitSpecificsId.Equals(unitspecificsId))
                            {
                                units.Count += count;
                            }
                        }
                    }
                });

            await dbContext.SaveChangesAsync();
        }

        public async Task<Unit> GetUnitBySpecificsAndLevelAsync(Guid countyId, Guid unitSpecificsId, int currentlvl)
        {
            var county = await dbContext.Counties
                .Include(c => c.Units)
                .SingleAsync(x => x.Id.Equals(countyId));

            return await dbContext.Units
                .SingleAsync(u => u.UnitSpecificsId.Equals(unitSpecificsId) && u.Level == currentlvl && u.UnitGroupId.Equals(county.Units.Id));
        }

        public async Task RemoveUnitGroup(Guid unitGroupId)
        {
            var unitgroup = await dbContext.UnitGroups.SingleAsync(x => x.Id.Equals(unitGroupId));

            dbContext.Remove(unitgroup);

            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UnitSpecifics>> GetAllUnitSpecificsAsync()
        {
            return await dbContext.UnitSpecifics
                .Include(u => u.UnitLevels)
                .ToListAsync();
        }

        public async Task RemoveUnitEntityByIdAsync(Guid unitId)
        {
            var entityToRemove = await dbContext.Units.SingleAsync(u => u.Id.Equals(unitId));

            dbContext.Remove(entityToRemove);

            await dbContext.SaveChangesAsync();
        }
    }
}
