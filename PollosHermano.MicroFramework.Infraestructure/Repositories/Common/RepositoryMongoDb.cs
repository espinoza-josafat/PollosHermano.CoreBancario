using PollosHermano.MicroFramework.Domain.Repositories;
using PollosHermano.MicroFramework.Entities;
using PollosHermano.MicroFramework.Entities.Models.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PollosHermano.MicroFramework.Infraestructure.Repositories.Common
{
    public class RepositoryMongoDb<TEntity> : IRepositoryMongoDb<TEntity> where TEntity : BaseEntity, new()
    {
        protected IMongoClient _client;
        protected IMongoDatabase _database;
        protected IMongoCollection<TEntity> _collection;
        readonly string _propertyNameKey;

        public RepositoryMongoDb(IMongoDbConnectionString connectionString)
        {
            var mongoUrl = new MongoUrl(connectionString.ConnectionString);

            _client = new MongoClient(mongoUrl);
            _database = _client.GetDatabase(mongoUrl.DatabaseName);
            _collection = _database.GetCollection<TEntity>(typeof(TEntity).Name);

            _propertyNameKey = GetPropertyNameKey();
        }

        protected string PropertyNameKey 
        { 
            get 
            { 
                return _propertyNameKey; 
            } 
        }

        protected string GetPropertyNameKey()
        {
            try
            {
                var properties = typeof(TEntity).GetProperties();

                foreach (var property in properties)
                    if (Attribute.GetCustomAttribute(property, typeof(KeyAttribute)) is KeyAttribute attribute)
                        return property.Name;
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return "Id";
        }

        public IEnumerable<TEntity> Get()
        {
            return _collection.Find(entity => true).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await (await _collection.FindAsync(entity => true)).ToListAsync();
        }

        public TEntity GetById(object id)
        {
            return _collection.Find(entity => entity[_propertyNameKey] == id).FirstOrDefault();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await (await _collection.FindAsync(entity => entity[_propertyNameKey] == id)).FirstOrDefaultAsync();
        }

        public TEntity Insert(TEntity entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public void Update(TEntity entity)
        {
            _collection.ReplaceOne(entityToUpdate => entityToUpdate[_propertyNameKey] == entity[_propertyNameKey], entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _collection.ReplaceOneAsync(entityToUpdate => entityToUpdate[_propertyNameKey] == entity[_propertyNameKey], entity);
        }


        public void Delete(TEntity entity)
        {
            _collection.DeleteOne(entityToDelete => entityToDelete[_propertyNameKey] == entity[_propertyNameKey]);
        }

        public void Delete(object id)
        {
            _collection.DeleteOne(entityToDelete => entityToDelete[_propertyNameKey] == id);
        }

        public async Task DeleteAsync(object id)
        {
            await _collection.DeleteOneAsync(entityToDelete => entityToDelete[_propertyNameKey] == id);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await _collection.DeleteOneAsync(entityToDelete => entityToDelete[_propertyNameKey] == entity[_propertyNameKey]);
        }
    }
}
