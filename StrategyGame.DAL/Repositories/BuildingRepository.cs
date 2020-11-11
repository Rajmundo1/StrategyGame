using StrategyGame.MODEL.Entities.Buildings;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.DAL.Repositories
{
    public class BuildingRepository: IBuildingRepository
    {
        private readonly ApplicationDbContext dbContext;
        //TODO
        //public BuildingRepository(ApplicationDbContext dbContext)
        //{
        //    this.dbContext = dbContext;
        //}

        //public async Task<IEnumerable<Building>> GetBuildingsAsync()
        //{
        //    return await dbContext.Buildings.Where(x => x.)
        //}

        //public async Task<PagedList<User>> GetFilteredUseresAsync(Expression<Func<User, bool>> filter, PagingParameters pagingParameters)
        //{
        //    return await dbContext.Users
        //        .Where(filter)
        //        .ToPagedListAsync(pagingParameters.PageNumber, pagingParameters.PageSize);
        //}
    }
}
