using Application.Interfaces.IUnitOfWorkService;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorkService
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DatabaseContext _dbContext;
        private IDbContextTransaction _transaction;

        //sample
        //public ITestAsyncRepository Test { get; private set; }
        public UnitOfWork(DatabaseContext dbContext)
        {
            _dbContext = dbContext;

            //sample
            //Test = new TestAsyncRepository(dbContext);

        }

        public async Task CompleteAsync()
        {
            var executionStrategy = _dbContext.Database.CreateExecutionStrategy();
            await executionStrategy.Execute(async () =>
            {
                using (_transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        await _dbContext.SaveChangesAsync();
                        await _transaction.CommitAsync();


                    }
                    catch (Exception e)
                    {

                        await _transaction.RollbackAsync();
                        _transaction?.DisposeAsync();
                        _dbContext.Dispose();
                    }
                }
            });

        }

        public void Dispose()
        {
            _transaction?.Dispose();

            _dbContext.Dispose();
        }
    }
}
