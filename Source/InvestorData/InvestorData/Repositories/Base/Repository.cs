using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvestorData
{

    /// <summary>
    /// Provides a base repository that manages database entities at a basic level,
    /// with a default entity key type of string.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class Repository<TEntity> : Repository<TEntity, string>, IRepository<TEntity> where TEntity : class
    {
        protected Repository(InvestorDbContext dbContext, DbSet<TEntity> dbSet) : base(dbContext, dbSet)
        {
            
        }
    }

    /// <summary>
    /// Provides a base repository that manages database entities at a basic level.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the entity key.</typeparam>
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {

        #region Protected Properties 

        protected InvestorDbContext DbContext { get; }

        protected DbSet<TEntity> DbSet { get; }

        #endregion


        #region Constructor

        protected Repository(InvestorDbContext dbContext, DbSet<TEntity> dbSet)
        {
            DbContext = dbContext;
            DbSet = dbSet;
        }

        #endregion


        #region CRUD Operations

        #region Read

        /// <inheritdoc/>
        public ValueTask<TEntity?> FindAsync(TKey key)
        {
            return DbSet.FindAsync(key);
        }

        /// <inheritdoc/>
        public virtual IAsyncEnumerable<TEntity> ListEntitiesAsync(Expression<Func<TEntity, bool>>? condition = null)
        {
            IQueryable<TEntity> query = condition is not null ? DbSet.Where(condition) : DbSet;

            return query.AsNoTracking()
                        .AsAsyncEnumerable();
        }

        #endregion

        #region Create, Update & Delete

        /// <inheritdoc/>
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        /// <inheritdoc/>
        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            var entry = DbContext.Entry(entity);

            if (entry.State == EntityState.Unchanged)
            {
                entry.State = EntityState.Modified;
            }

            return await DbContext.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public virtual async Task<DeleteResult> DeleteAsync(TKey key)
        {
            var entity = await DbSet.FindAsync(key);
            if (entity is null)
            {
                return DeleteResult.EntityNotFound;
            }

            DbSet.Remove(entity);
            return await DbContext.SaveChangesAsync() > 0 ? DeleteResult.Success : DeleteResult.Failed;
        }

        #endregion

        #endregion


        #region Helper Methods

        /// <inheritdoc/>
        public bool EntityExists(TKey key) => DbSet.Any(HasKey(key));

        #endregion


        #region Abstract Methods

        /// <summary>
        /// Returns the entity property representing the key. 
        /// </summary>
        protected abstract Expression<Func<TEntity, TKey>> KeyProperty();

        /// <summary>
        /// Returns the expression of key-matching predicate. 
        /// </summary>
        protected abstract Expression<Func<TEntity, bool>> HasKey(TKey key);

        #endregion

    }
}
