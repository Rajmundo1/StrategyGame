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

            var unitsToAdd = new List<Unit>();

            for(int i = 0; i<count; i++)
            {
                unitsToAdd.Add(new Unit
                {
                    Id = Guid.NewGuid(),
                    Level = 1,
                    UnitSpecificsId = unitSpecificsId,
                    UnitGroupId = county.Units.Id
                });
            }

            await dbContext.Units.AddRangeAsync(unitsToAdd);

            await dbContext.SaveChangesAsync();
        }

        public async Task DevelopUnitsAsync(int count, Guid countyId, Guid unitSpecificsId, int currentLvl)
        {
            var specificUnits = new List<Unit>();

            await Task.Run(() =>
            {
                specificUnits = dbContext.UnitGroups
                .Include(ug => ug.Units)
                .Single(unitGroup => unitGroup.CountyId.Equals(countyId))
                .Units
                .Where(units => units.UnitSpecificsId.Equals(unitSpecificsId) && units.Level == currentLvl)
                .ToList();
            });

            var unitsToDevelop = new List<Unit>();

            for (int i = 0; i < count; i++)
            {
                unitsToDevelop.Add(specificUnits[0]);
                specificUnits.RemoveAt(0);
            }

            await Task.Run(() =>
            {
                dbContext.UnitGroups
                 .Include(ug => ug.Units)
                 .Single(unitGroup => unitGroup.CountyId.Equals(countyId))
                 .Units
                 .Where(x => unitsToDevelop.Contains(x))
                 .ToList()
                 .ForEach(unit => unit.Level += 1);
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
            var specificUnits = new List<Unit>();

            await Task.Run(async () =>
            {
                var unitGroup = await dbContext.UnitGroups
                .Include(ug => ug.Units)
                .ThenInclude(u => u.UnitSpecifics)
                .ThenInclude(usp => usp.UnitLevels)
                .SingleAsync(unitGroup => unitGroup.CountyId.Equals(countyId));

                specificUnits =
                    unitGroup
                    .Units
                    .Where(units => units.UnitSpecifics.Id.Equals(unitSpecificsId) && units.Level == lvl)
                    .ToList();
            });

            var unitsToRemove = new List<Unit>();

            for(int i = 0; i<count; i++)
            {
                unitsToRemove.Add(specificUnits[0]);
                specificUnits.RemoveAt(0);
            }

            await Task.Run(() =>
            {
                dbContext.Units
                    .RemoveRange(unitsToRemove);

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
            dbContext.Units.Remove(unitToRemove);

            await dbContext.SaveChangesAsync();
        }

        public async Task MoveToUnitGroup(Guid unitId, Guid unitGroupId)
        {
            await dbContext.Units.ForEachAsync(unit =>
            {
                if (unit.Id.Equals(unitId))
                {
                    unit.UnitGroupId = unitGroupId;
                }
            });

            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Unit>> GetUnitsBySpecificsAndLevelAsync(Guid countyId, Guid unitSpecificsId, int currentlvl)
        {
            return await Task.Run(async () =>
            {
                var unitGorup = await dbContext.UnitGroups
                    .Include(ug => ug.Units)
                    .ThenInclude(u => u.UnitSpecifics)
                    .ThenInclude(usp => usp.UnitLevels)
                    .SingleAsync(unit => unit.CountyId.Equals(countyId));

                return unitGorup
                    .Units
                    .Where(unit => unit.UnitSpecificsId.Equals(unitSpecificsId) && unit.Level.Equals(currentlvl))
                    .ToList();
            });
        }

        public async Task RemoveUnitGroup(Guid unitGroupId)
        {
            var unitgroup = await dbContext.UnitGroups.SingleAsync(x => x.Id.Equals(unitGroupId));

            dbContext.Remove(unitgroup);

            await dbContext.SaveChangesAsync();
        }
    }
}
