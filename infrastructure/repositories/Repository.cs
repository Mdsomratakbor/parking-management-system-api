using infrastructure;
using Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Creates a row in the table.
        /// </summary>
        /// <param name="entity">Object to be saved in the table as a row.</param>
        /// <returns>Saves object.</returns>
        public T Add(T entity)
        {
            try
            {
                return context.Set<T>().Add(entity).Entity;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns a row from the table as an object if primary key matches.
        /// </summary>
        /// <param name="id">Primary key.</param>
        /// <returns>Retrieves object.</returns>
        public T Get(Guid id)
        {
            try
            {
                return context.Set<T>().Find(id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns a row from the table as an object if primary key matches.
        /// </summary>
        /// <param name="id">Primary key.</param>
        /// <returns>Retrieves object.</returns>
        public T Get(int id)
        {
            try
            {
                return context.Set<T>().Find(id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns all rows as a list of objects from the table.
        /// </summary>
        /// <returns>List of objects.</returns>
        public IEnumerable<T> GetAll()
        {
            try
            {
                return context.Set<T>()
                    .AsQueryable()
                    .AsNoTracking()
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add multi object to database
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }

        /// <summary>
        /// Returns matched rows as a list of objects.
        /// </summary>
        /// <param name="predicate">Custom LINQ expression.</param>
        /// <returns>List of objects.</returns>
        public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await context.Set<T>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(predicate)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns matched rows as a list of objects.
        /// </summary>
        /// <param name="predicate">Custom LINQ expression.</param>
        /// <returns>List of objects.</returns>
        public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj)
        {
            try
            {
                return await context.Set<T>()
                    .AsQueryable()
                    .Where(predicate)
                    .Include(obj)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns matched rows as a list of objects.
        /// </summary>
        /// <param name="predicate">Custom LINQ expression.</param>
        /// <returns>List of objects.</returns>
        public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj, Expression<Func<T, object>> next)
        {
            try
            {
                return await context.Set<T>()
                      .AsQueryable()
                      .Where(predicate)
                      .Include(obj)
                      .Include(next)
                      .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns first matched row as an object from the table.
        /// </summary>
        /// <param name="predicate">Custom LINQ expression.</param>
        /// <returns>Retrieved object.</returns>
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await context.Set<T>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns first matched row as an object from the table.
        /// </summary>
        /// <param name="predicate">Custom LINQ expression.</param>
        /// <returns>Retrieved object.</returns>
        public async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await context.Set<T>()
                    .AsNoTracking()
                    .OrderByDescending(predicate)
                    .FirstOrDefaultAsync(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a row in the table.
        /// </summary>
        /// <param name="entity">Object to be updated.</param>
        public void Update(T entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                context.Set<T>().Update(entity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a row from the table.
        /// </summary>
        /// <param name="entity">Object to be deleted.</param>
        public void Delete(T entity)
        {
            try
            {
                context.Set<T>().Remove(entity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Include child a row from the table.
        /// </summary>
        /// <param name="entity">Object to be deleted.</param>
        public async Task<T> LoadWithChildWithOrderByAsync<TEntity>(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] expressionList)
        {
            var query = context.Set<T>().AsQueryable();

            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            if (orderBy != null)
                query = orderBy(query);

            return await query.FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Loads a single entity of type <typeparamref name="T"/> along with its related child entities based on the provided predicate and included child expressions.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="predicate">The filter criteria for the entity.</param>
        /// <param name="expressionList">The list of child entity expressions to include.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity or null if no entity is found.</returns>
        public async Task<T> LoadWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] expressionList)
        {
            var query = context.Set<T>().AsQueryable();

            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Loads a list of entities of type <typeparamref name="T"/> along with their related child entities based on the provided predicate and included child expressions.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="predicate">The filter criteria for the entities.</param>
        /// <param name="expressionList">The list of child entity expressions to include.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of matching entities.</returns>
        public async Task<IEnumerable<T>> LoadListWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] expressionList)
        {
            var query = context.Set<T>().AsQueryable();

            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            return await query.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Loads a paginated list of entities of type <typeparamref name="T"/> along with their related child entities based on the provided predicate and included child expressions.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="predicate">The filter criteria for the entities.</param>
        /// <param name="skip">The number of records to skip.</param>
        /// <param name="take">The number of records to take.</param>
        /// <param name="orderBy">Optional ordering function for the entities.</param>
        /// <param name="expressionList">The list of child entity expressions to include.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of matching entities.</returns>
        public async Task<IEnumerable<T>> LoadListWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, int skip, int take, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] expressionList)
        {
            var query = context.Set<T>().AsQueryable();

            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }
            if (orderBy != null)
                query = orderBy(query);

            return await query.Where(predicate).Skip(skip).Take(take).ToListAsync();
        }

        /// <summary>
        /// Loads a grouped and paginated list of entities of type <typeparamref name="T"/> along with their related child entities based on the provided predicate and included child expressions.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="predicate">The filter criteria for the entities.</param>
        /// <param name="groupByKeySelector">A function to select the key for grouping.</param>
        /// <param name="skip">The number of records to skip.</param>
        /// <param name="take">The number of records to take.</param>
        /// <param name="orderBy">Optional ordering function for the entities.</param>
        /// <param name="expressionList">The list of child entity expressions to include.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a grouped list of matching entities.</returns>
        public async Task<IEnumerable<IGrouping<string, T>>> LoadGroupedListWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, Func<T, string> groupByKeySelector, int skip, int take, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] expressionList)
        {
            var query = context.Set<T>().AsQueryable();

            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            if (orderBy != null)
                query = orderBy(query);

            var groupedQuery = query.Where(predicate).GroupBy(groupByKeySelector).AsEnumerable().Skip(skip).Take(take);

            return await Task.Run(() => groupedQuery);
        }

        /// <summary>
        /// Counts the number of entities of type <typeparamref name="T"/> that match the given filter and includes child entities as specified.
        /// </summary>
        /// <param name="filter">The filter criteria for the entities.</param>
        /// <param name="expressionList">The list of child entity expressions to include.</param>
        /// <returns>The count of matching entities.</returns>
        public int Count(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] expressionList)
        {
            foreach (var expression in expressionList)
            {
                context.Set<T>().Include(expression);
            }

            return context.Set<T>().Where(filter).Count();

        }

        /// <summary>
        /// Retrieves an entity of type <typeparamref name="T"/> by its unique GUID identifier.
        /// </summary>
        /// <param name="id">The GUID identifier of the entity.</param>
        /// <returns>The entity if found, otherwise null.</returns>
        public virtual T GetById(Guid id)
        {
            try
            {
                var entity = context.Set<T>().Find(id);
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves an entity of type <typeparamref name="T"/> by its integer key.
        /// </summary>
        /// <param name="Key">The integer identifier of the entity.</param>
        /// <returns>The entity if found, otherwise null.</returns>
        public T GetById(long Key)
        {
            try
            {
                var entity = context.Set<T>().Find(Key);
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves an entity of type <typeparamref name="T"/> by its unique GUID identifier.
        /// </summary>
        /// <param name="id">The GUID identifier of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity or null if no entity is found.</returns>
        public async Task<T?> GetByIdAsync(Guid id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        /// <summary>
        /// Asynchronously retrieves an entity of type <typeparamref name="T"/> by its integer key.
        /// </summary>
        /// <param name="key">The integer identifier of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity or null if no entity is found.</returns>
        public async Task<T?> GetByIdAsync(long key)
        {
            var entity = await context.Set<T>().FindAsync(key);
            if (entity == null)
            {
                return null;
            }

            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        public async Task<T?> GetByIdAsync(int key)
        {
            var entity = await context.Set<T>().FindAsync(key);
            if (entity == null)
            {
                return null;
            }

            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

    }

    public class EntityBase
    {
        public int Id { get; set; }
    }
}