using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemRepository : IItemRepository
    {
        private const string _databaseName="Catalog";
        private const string _collectionName="Items";
        private readonly IMongoCollection<Item> _itemCollection;

        private readonly FilterDefinitionBuilder<Item> filterBuilder= Builders<Item>.Filter;
        public MongoDbItemRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database=mongoClient.GetDatabase(_databaseName);
            _itemCollection = database.GetCollection<Item>(_collectionName); 
        }
        public async Task CreateItemAsync(Item item)
        {
            await _itemCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
           var filter =filterBuilder.Eq(x=>x.Id,id);
           await _itemCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter =filterBuilder.Eq(x=>x.Id,id);
            var result= await _itemCollection.FindAsync(filter);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            var result= await _itemCollection.FindAsync(new BsonDocument());
            return result.ToList();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter =filterBuilder.Eq(x=>x.Id,item.Id);
            await _itemCollection.ReplaceOneAsync(filter,item);

        }
    }
}