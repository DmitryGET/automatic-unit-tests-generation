using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure.Database;

namespace SampleProject.Infrastructure.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("The transaction has already been started");
            }

            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            return _transaction;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("The transaction hasn't already been started");
            }

            await _transaction.CommitAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            _transaction.Dispose();
            _transaction = null;
        }
        
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("The transaction hasn't already been started");
            }

            await _transaction.RollbackAsync(cancellationToken);
            
            _transaction.Dispose();
            _transaction = null;
        }
    }
}