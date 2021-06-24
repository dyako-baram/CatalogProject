using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<ItemDto> GetItems()
        {
            return _repository.GetItems().Select(x=> x.AsDto());

        }
        [HttpGet("{id}")]
        public IActionResult GetItem(Guid id)
        {
            
            var item= _repository.GetItem(id);
            if(item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());
        }
        [HttpPost]
        public IActionResult CreateItem(CreateItemDto itemDto)
        {
            Item item=new(){
                Id=Guid.NewGuid(),
                Name=itemDto.Name,
                Price=itemDto.Price,
                CreatedDate=DateTimeOffset.UtcNow
            };
            _repository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem),new{id=item.Id},item.AsDto());
        }
        [HttpPut("{id}")]
        public IActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var item=_repository.GetItem(id);
            if(item is null)
            {
                return NotFound();
            }
            Item updateItem=item with{
                Name=itemDto.Name,
                Price=itemDto.Price
            };
            _repository.UpdateItem(updateItem);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(Guid id)
        {
            var item=_repository.GetItem(id);
            if(item is null)
            {
                return NotFound();
            }
            _repository.DeleteItem(id);
            return NoContent();
        }
    }
}