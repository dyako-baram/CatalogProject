using System;
using System.Collections.Generic;
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
        public void CreateItem(Item item)
        {
            _itemCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
           var filter =filterBuilder.Eq(x=>x.Id,id);
           _itemCollection.DeleteOne(filter);
        }

        public Item GetItem(Guid id)
        {
            var filter =filterBuilder.Eq(x=>x.Id,id);
            return _itemCollection.Find(filter).FirstOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return _itemCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter =filterBuilder.Eq(x=>x.Id,item.Id);
            _itemCollection.ReplaceOne(filter,item);

        }
    }
}