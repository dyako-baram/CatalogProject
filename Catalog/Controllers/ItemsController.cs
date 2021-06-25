using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _repository;
        public ItemsController(IItemRepository repository)
        {
            _repository=repository;
        }
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var result=await _repository.GetItemsAsync();
            return result.Select(x=> x.AsDto());

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemAsync(Guid id)
        {
            
            var item=await _repository.GetItemAsync(id);
            if(item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());
        }
        [HttpPost]
        public async Task<IActionResult> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item=new(){
                Id=Guid.NewGuid(),
                Name=itemDto.Name,
                Price=itemDto.Price,
                CreatedDate=DateTimeOffset.UtcNow
            };
            await _repository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItemAsync),new{id=item.Id},item.AsDto());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var item=await _repository.GetItemAsync(id);
            if(item is null)
            {
                return NotFound();
            }
            Item updateItem=item with{
                Name=itemDto.Name,
                Price=itemDto.Price
            };
            await _repository.UpdateItemAsync(updateItem);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemAsync(Guid id)
        {
            var item=_repository.GetItemAsync(id);
            if(item is null)
            {
                return NotFound();
            }
            await _repository.DeleteItemAsync(id);
            return NoContent();
        }
    }
}