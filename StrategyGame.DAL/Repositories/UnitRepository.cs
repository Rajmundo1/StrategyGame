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
            var county = await dbContext.Counties.SingleAsync(x => x.Id.Equals(countyId));

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
        }

        public async Task DevelopUnitsAsync(int count, Guid countyId, Guid unitSpecificsId, int currentLvl)
        {
            var specificUnits = new List<Unit>();

            await Task.Run(() =>
            {
                specificUnits = dbContext.UnitGroups
                .Single(unitGroup => unitGroup.CountyId.Equals(countyId))
                .Units
                .Where(units => units.UnitSpecifics.Id.Equals(unitSpecificsId) && units.Level == currentLvl)
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
                 .Single(unitGroup => unitGroup.CountyId.Equals(countyId))
                 .Units.ToList()
                 .Where(x => unitsToDevelop.Contains(x))
                 .ToList()
                 .ForEach(unit => unit.Level++);
            });
        }

        public async Task<IEnumerable<Unit>> GetUnitsAsync(Guid countyId)
        {
            return await Task.Run(() =>
            {
                return dbContext.UnitGroups
                .Single(unitGroup => unitGroup.CountyId.Equals(countyId))
                .Units;
            });
        }

        public async Task RemoveUnitsAsync(int count, Guid countyId, Guid unitSpecificsId, int lvl)
        {
            var specificUnits = new List<Unit>();

            await Task.Run(() =>
            {
                specificUnits = dbContext.UnitGroups
                .Single(unitGroup => unitGroup.CountyId.Equals(countyId))
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
                dbContext.UnitGroups
                 .Single(unitGroup => unitGroup.CountyId.Equals(countyId))
                 .Units.ToList()
                 .RemoveAll(x => unitsToRemove.Contains(x));
            });
        }

        public async Task<UnitSpecifics> GetUnitSpecificsAsync(Guid unitSpecificsId)
        {
            return await dbContext.UnitSpecifics.SingleAsync(x => x.Id.Equals(unitSpecificsId));
        }

        public async Task RemoveUnitByIdAsync(Guid unitId)
        {
            var unitToRemove = await dbContext.Units.SingleAsync(x => x.Id.Equals(unitId));
            dbContext.Units.Remove(unitToRemove);
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
        }

        public async Task<IEnumerable<Unit>> GetUnitsBySpecificsAndLevelAsync(Guid countyId, Guid unitSpecificsId, int currentlvl)
        {
            return await Task.Run(() =>
            {
                return dbContext.UnitGroups
                    .Single(unit => unit.CountyId.Equals(countyId))
                    .Units
                    .Where(unit => unit.UnitSpecificsId.Equals(unitSpecificsId) && unit.Level.Equals(currentlvl))
                    .ToList();
            });
        }
    }
}
