using ActivityTracker.Backend.Repository.Domain.Context;
using ActivityTracker.Backend.Repository.Domain.Documents.Base;
using ActivityTracker.Backend.Repository.Domain.Settings;
using ActivityTracker.Backend.Repository.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ActivityTracker.Backend.Repository.Repositories
{
    public class GenericMongoRepository<T> : IGenericMongoRepository<T> where T : BaseDocument
    {
        #region Fields

        private readonly IMongoCollection<T> _collection;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="settings">The settings for the MongoDB</param>
        public GenericMongoRepository(MongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<T>(GenericMongoRepository<T>._getCollectionNameHelper(typeof(T)));
        }

        #endregion

        public IQueryable<T> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public async Task<IEnumerable<T>> FilterBy(Expression<Func<T, bool>> filterExpression)
        {
            var result = await _collection.Find(filterExpression).ToListAsync();
            return result;
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            var result = await _collection.Find(filterExpression).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Tuple<int, IEnumerable<T>>> FindManyPagedAsync(Expression<Func<T, bool>> filterExpression, int pageNumber, int pageSize)
        {
            var getCollectionCount = _collection
                      .Find(filterExpression)
                      .CountDocumentsAsync();

            var items = await _collection
                .Find(filterExpression)
                .Limit(pageSize)
                .Skip((pageNumber - 1) * pageSize)
                .ToListAsync();

            return new Tuple<int, IEnumerable<T>>(Convert.ToInt32(await getCollectionCount), items);
        }

        public async Task<T> FindByIdAsync(string id)
        {
            var result = await _collection.Find(x => x.Id == id).SingleOrDefaultAsync();
            return result;
        }

        public async Task InsertOneAsync(T document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task ReaplceOneAsync(T document)
        {
            await _collection.FindOneAndReplaceAsync(doc => doc.Id == document.Id, document);
        }

        public async Task DeleteByIdAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        #region Helper methods

        private protected static string _getCollectionNameHelper(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                        typeof(BsonCollectionAttribute), true).FirstOrDefault())?.CollectionName;
        }

        #endregion
    }
}
