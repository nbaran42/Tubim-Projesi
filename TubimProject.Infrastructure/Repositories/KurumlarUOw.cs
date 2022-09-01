using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Context;
using TubimProject.Application.Interfaces.Repositories;
using TubimProject.Application.Interfaces.Shared;

namespace TubimProject.Infrastructure.Repositories
{
    public class KurumlarUOw : IUnitOfWork
    {
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly KurumlarContext _dbContext;
        private bool disposed;

        public KurumlarUOw(IAuthenticatedUserService authenticatedUserService, KurumlarContext dbContext)
        {
            _authenticatedUserService=authenticatedUserService;
            _dbContext= dbContext ?? throw new ArgumentNullException(nameof(dbContext)); ;
        }

        public async Task<int> Commit(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task Rollback()
        {
            //todo
            return Task.CompletedTask;
        }

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }
    }
}
