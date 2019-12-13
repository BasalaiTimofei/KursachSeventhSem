using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[base]")]
    [ApiController]
    public abstract class BaseController<TEntity, TRepository> : Controller
        where TEntity : class, IEntity 
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository _repository;

        protected BaseController(TRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await _repository.GetAll();
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<TEntity>> Get(string id)
        {
            var entity = await _repository.Get(id);
            if (entity == null) { return BadRequest(); }

            return entity;
        }

        [HttpPut("put/{id}")]
        public async Task<IActionResult> Put(string id, TEntity entity)
        {
            if (!string.Equals(id, entity.Id, StringComparison.InvariantCultureIgnoreCase))
            {
                return BadRequest();
            }

            await _repository.Update(entity);
            return NoContent();
        }

        [HttpPost("post")]
        public async Task<ActionResult<IEntity>> Post(TEntity entity)
        {
            await _repository.Add(entity);

            //return CreatedAtAction("Get", new {id = entity.Id}, entity);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<IEntity>> Delete(string id)
        {
            var entity = await _repository.Delete(id);
            if (entity == null) { return NotFound(); }

            return entity;
        }
    }
}