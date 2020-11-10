using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.MODEL.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
