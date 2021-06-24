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
    }
}