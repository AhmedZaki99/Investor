using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvestorAPI.Data
{

    /// <summary>
    /// Provides a base data store that manages database entities at a basic level,
    /// with a default entity key type of string.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    internal abstract class EntityStore<TEntity> : EntityStore<TEntity, string>, IEntityStore<TEntity> where TEntity : class
    {
        protected EntityStore(ApplicationDbContext dbContext, DbSet<TEntity> dbSet) : base(dbContext, dbSet)
        {
            
        }
    }

    /// <summary>
    /// Provides a base data store that manages database entities at a basic level.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the entity key.</typeparam>
    internal abstract class EntityStore<TEntity, TKey> : IEntityStore<TEntity, TKey> where TEntity : class
    {

        #region Public Properties

        private int _maxEntitiesPerPage = 100;
        /// <summary>
        /// Gets or sets the maximum number of entities to list in a page.
        /// </summary>
        /// <remarks>
        /// Defaults to 100.
        /// </remarks>
        public int MaxEntitiesPerPage
        {
            get => _maxEntitiesPerPage;
            set
            {
                if (_maxEntitiesPerPage == value) return;
                if (value <= 0)
                {
                    throw new InvalidOperationException("Value provided must be a positive integer.");
                }
                _maxEntitiesPerPage = value;
            }
        }

        #endregion

        #region Protected Properties 

        protected ApplicationDbContext DbContext { get; }

        protected DbSet<TEntity> DbSet { get; }

        #endregion


        #region Constructor

        protected EntityStore(ApplicationDbContext dbContext, DbSet<TEntity> dbSet)
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
        public virtual Task<TEntity?> FindAndNavigateAsync(TKey key, bool track = false)
        {
            IQueryable<TEntity> query = GetNavigationQuery();
            if (!track)
            {
                query = query.AsNoTracking();
            }
            return query.FirstOrDefaultAsync(HasKey(key));
        }


        /// <inheritdoc/>
        public virtual Task<List<TEntity>> ListEntitiesAsync(int page = 1, int entitiesPerPage = 30)
        {
            entitiesPerPage = entitiesPerPage > MaxEntitiesPerPage ? MaxEntitiesPerPage : entitiesPerPage;

            return GetOrderedQuery().Skip((page - 1) * entitiesPerPage)
                                    .Take(entitiesPerPage)
                                    .AsNoTracking()
                                    .ToListAsync();
        }

        /// <inheritdoc/>
        public virtual Task<List<TEntity>> PaginateEntitiesAsync(TEntity? lastEntity = null, int entitiesPerPage = 30)
        {
            entitiesPerPage = entitiesPerPage > MaxEntitiesPerPage ? MaxEntitiesPerPage : entitiesPerPage;

            IQueryable<TEntity> query = GetOrderedQuery();
            if (lastEntity is not null)
            {
                query = query.Where(OrderedAfterEntity(lastEntity));
            }

            return query.Take(entitiesPerPage)
                        .AsNoTracking()
                        .ToListAsync();
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

        #region Virtual Helper Methods

        /// <summary>
        /// Get the query representing the entier DbSet, including all navigation properties.
        /// </summary>
        /// <remarks>
        /// This method shoud be overriden by deriving classes if the entity includes any navigation properties,
        /// since the default base implementation only returns the DbSet as is, with no relations included.
        /// </remarks>
        protected virtual IQueryable<TEntity> GetNavigationQuery()
        {
            return DbSet.AsQueryable();
        }

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


        /// <summary>
        /// Get the query representing the entities of the DbSet ordered for pagination.
        /// </summary>
        protected abstract IOrderedQueryable<TEntity> GetOrderedQuery();

        /// <summary>
        /// Returns the expression that determines wheather entities are ordered
        /// after a given entity.
        /// </summary>
        protected abstract Expression<Func<TEntity, bool>> OrderedAfterEntity(TEntity entity);

        #endregion

    }
}
