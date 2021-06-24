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

        public void CreateItem(Item item)
        {
            items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            var index=items.FindIndex(x=>x.Id==item.Id);
            items[index]=item;
        }

        public void DeleteItem(Guid id)
        {
            var index=items.FindIndex(x=>x.Id==id);
            items.RemoveAt(index);
        }
    }
}