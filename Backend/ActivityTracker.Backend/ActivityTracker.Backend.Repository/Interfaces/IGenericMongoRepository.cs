using ActivityTracker.Backend.Repository.Domain.Documents.Base;
using System.Linq.Expressions;

namespace ActivityTracker.Backend.Repository.Interfaces
{
    public interface IGenericMongoRepository<T> where T : BaseDocument
    {
        /// <summary>
        /// Gets IQueryable of T.
        /// </summary>
        /// <returns>IQueryable of T</returns>
        IQueryable<T> AsQueryable();

        /// <summary>
        /// Gets a a list of documents.
        /// </summary>
        /// <param name="filterExpression">The expression filter</param>
        /// <returns>Documents</returns>
        Task<IEnumerable<T>> FilterBy(
            Expression<Func<T, bool>> filterExpression);

        /// <summary>
        /// Gets a single document by the filter expression.
        /// </summary>
        /// <param name="filterExpression">The expression filter</param>
        /// <returns>A single document</returns>
        Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression);

        /// <summary>
        /// Gets a total amount of documents and a list of documents in a tuple.
        /// </summary>
        /// <param name="filterExpression">The expression filter</param>
        /// <param name="pageNumber">The page number</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>Item1: Total count - Item2: documents</returns>
        Task<Tuple<int, IEnumerable<T>>> FindManyPagedAsync(Expression<Func<T, bool>> filterExpression, int pageNumber, int pageSize);

        /// <summary>
        /// Gets a single document by ID.
        /// </summary>
        /// <param name="id">The document Id</param>
        /// <returns>A single document</returns>
        Task<T> FindByIdAsync(string id);

        /// <summary>
        /// Inserts a new document.
        /// </summary>
        /// <param name="document">The document</param>
        /// <returns>On failure a exception will be trowed</returns>
        Task InsertOneAsync(T document);

        /// <summary>
        /// Replacing an existing document.
        /// </summary>
        /// <param name="document">The document</param>
        /// <returns>On failure a exception will be trowed</returns>
        Task ReaplceOneAsync(T document);

        /// <summary>
        /// Deletes an existing document.
        /// </summary>
        /// <param name="document">The id of the document</param>
        /// <returns>On failure a exception will be trowed</returns>
        Task DeleteByIdAsync(string id);

    }
}
