using System;
using System.Linq;
using System.Collections.Generic;
using Catalog.Entities;

namespace Catalog.Repositories
{

    public class InMemItemRepository : IItemRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Posion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }
        };
        public IEnumerable<Item> GetItems()
        {
            return items;
        }
        public Item GetItem(Guid id)
        {
            return items.FirstOrDefault<Item>(x => x.Id == id);
        }
    }
}