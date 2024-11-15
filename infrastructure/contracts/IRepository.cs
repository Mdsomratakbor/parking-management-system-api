using System.Linq.Expressions;
namespace Infrastructure.Contracts
{
    /// <summary>
    /// Contains signatures of all generic methods.
    /// </summary>
    /// <typeparam name="T">T is a model class.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Adds a new entity to the database table.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity.</returns>
        T Add(T entity);

        /// <summary>
        /// Retrieves an entity by its primary key from the database table.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        /// <returns>The entity with the specified primary key.</returns>
        T Get(Guid id);

        /// <summary>
        /// Retrieves an entity by its primary key from the database table.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        /// <returns>The entity with the specified primary key.</returns>
        T Get(int id);

        /// <summary>
        /// Retrieves all entities from the database table.
        /// </summary>
        /// <returns>A collection of all entities.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Adds a collection of entities to the database table.
        /// </summary>
        /// <param name="entities">The collection of entities to add.</param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// Queries the database table based on the provided LINQ expression.
        /// </summary>
        /// <param name="predicate">The LINQ expression to filter data.</param>
        /// <param name="obj">Additional objects to include in the query result.</param>
        /// <returns>A collection of entities matching the query criteria.</returns>
        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj);

        /// <summary>
        /// Queries the database table based on the provided LINQ expression.
        /// </summary>
        /// <param name="predicate">The LINQ expression to filter data.</param>
        /// <param name="obj">Additional objects to include in the query result.</param>
        /// <param name="next">Additional objects to include in the query result.</param>
        /// <returns>A collection of entities matching the query criteria.</returns>
        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj, Expression<Func<T, object>> next);

        /// <summary>
        /// Queries the database table based on the provided LINQ expression.
        /// </summary>
        /// <param name="predicate">The LINQ expression to filter data.</param>
        /// <returns>A collection of entities matching the query criteria.</returns>
        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retrieves the first entity matching the provided LINQ expression from the database table.
        /// </summary>
        /// <param name="predicate">The LINQ expression to filter data.</param>
        /// <returns>The first entity matching the query criteria.</returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retrieves the last entity matching the provided LINQ expression from the database table.
        /// </summary>
        /// <param name="predicate">The LINQ expression to filter data.</param>
        /// <returns>The last entity matching the query criteria.</returns>
        Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Updates an entity in the database table.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes an entity from the database table.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);

        /// <summary>
        /// Retrieves an entity by its primary key from the database table.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        /// <returns>The entity with the specified primary key.</returns>
        T GetById(Guid id);

        /// <summary>
        /// Retrieves an entity by its primary key from the database table.
        /// </summary>
        /// <param name="key">The primary key value.</param>
        /// <returns>The entity with the specified primary key.</returns>
        T GetById(long key);

        /// <summary>
        /// Asynchronously retrieves an entity by its primary key from the database table.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity with the specified primary key, if found.</returns>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// Asynchronously retrieves an entity by its primary key from the database table.
        /// </summary>
        /// <param name="key">The primary key value.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity with the specified primary key, if found.</returns>
        Task<T?> GetByIdAsync(long key);
        Task<T?> GetByIdAsync(int key);

        /// <summary>
        /// Retrieves the count of entities in the database table based on the specified filter criteria.
        /// </summary>
        /// <param name="filter">The LINQ expression to filter data.</param>
        /// <param name="expressionList">Additional expressions to include in the query result.</param>
        /// <returns>The count of entities that match the filter criteria.</returns>
        int Count(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] expressionList);

        /// <summary>
        /// Loads a single entity with related child entities from the database table based on the provided filter criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of child entity to load.</typeparam>
        /// <param name="predicate">The LINQ expression to filter data.</param>
        /// <param name="expressionList">Additional expressions to include in the query result.</param>
        /// <returns>The entity with related child entities.</returns>
        Task<T> LoadWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] expressionList);

        /// <summary>
        /// Loads a collection of entities with related child entities from the database table based on the provided filter criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of child entity to load.</typeparam>
        /// <param name="predicate">The LINQ expression to filter data.</param>
        /// <param name="expressionList">Additional expressions to include in the query result.</param>
        /// <returns>A collection of entities with related child entities.</returns>
        Task<IEnumerable<T>> LoadListWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] expressionList);

        /// <summary>
        /// Loads a paged collection of entities with related child entities from the database table based on the provided filter criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of child entity to load.</typeparam>
        /// <param name="predicate">The LINQ expression to filter data.</param>
        /// <param name="skip">The number of entities to skip.</param>
        /// <param name="take">The number of entities to return.</param>
        /// <param name="orderBy">The ordering expression for the query result.</param>
        /// <param name="expressionList">Additional expressions to include in the query result.</param>
        /// <returns>A paged collection of entities with related child entities.</returns>
        Task<IEnumerable<T>> LoadListWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, int skip, int take, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] expressionList);
    }
}