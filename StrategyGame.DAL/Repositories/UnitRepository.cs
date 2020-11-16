using Microsoft.EntityFrameworkCore;
using StrategyGame.MODEL.Entities.Units;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task DevelopUnits(int count, Guid countyId, Guid unitSpecificsId)
        {
            var specificUnits = new List<Unit>();

            await Task.Run(() =>
            {
                specificUnits = dbContext.UnitGroups
                .Single(unitGroup => unitGroup.CountyId.Equals(countyId))
                .Units
                .Where(units => units.UnitSpecifics.Id.Equals(unitSpecificsId))
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
                 .ForEach(unit => unit.UnitSpecifics.Level++);
            });
        }

        public async Task<IEnumerable<Unit>> GetUnits(Guid countyId)
        {
            return await Task.Run(() =>
            {
                return dbContext.UnitGroups
                .Single(unitGroup => unitGroup.CountyId.Equals(countyId))
                .Units;
            });
        }

        public async Task RemoveUnits(int count, Guid countyId, Guid unitSpecificsId)
        {
            var specificUnits = new List<Unit>();

            await Task.Run(() =>
            {
                specificUnits = dbContext.UnitGroups
                .Single(unitGroup => unitGroup.CountyId.Equals(countyId))
                .Units
                .Where(units => units.UnitSpecifics.Id.Equals(unitSpecificsId))
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
    }
}
