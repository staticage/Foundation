
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Foundation.CQRS;
using Foundation.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Example.Applications.Api.Controllers
{
    //[ApiController]
    public abstract class EntityApiControllerBase<TEntityForm, TEntity, TEntityResource> : Controller
        where TEntity : class
        where TEntityResource : class
    {
        private readonly IServiceBus _serviceBus;
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;
    
        protected EntityApiControllerBase(IServiceBus serviceBus, DbContext dbContext, IMapper mapper)
        {
            _serviceBus = serviceBus;
            _dbContext = dbContext;
            _mapper = mapper;
        }
    
        [HttpGet("{id}")]
        public async Task<TEntityResource> Get(int id)
        {
            var entity = await _dbContext.FindAsync<TEntity>(id);
            return _mapper.Map<TEntityResource>(entity);
        }
    
        [HttpPost]
        public async Task Create(CreateEntityCommand<TEntityForm> command)
        {
            await _serviceBus.Send(command);
        }
    
        [HttpPut]
        public async Task Update(UpdateEntityCommand<TEntityForm> command)
        {
            await _serviceBus.Send(command);
        }
    
        [HttpDelete]
        public async Task Delete(DeleteEntityCommand<TEntityForm> command)
        {
            await _serviceBus.Send(command);
        }
    
        [HttpPut("{id}")]
        public async Task Update([FromQuery]long id, UpdateEntityCommand<TEntityForm> command)
        {
            command.Id = id;
            await Update(command);
        }
    
        [HttpDelete("{id}")]
        public async Task Delete([FromQuery]long id)
        {
            await Delete(new DeleteEntityCommand<TEntityForm> { Id = id });
        }
    }

    public class CreateEntityCommand<TEntityForm> : Command
    {
        [FromBody]
        public TEntityForm Form { get; set; }
    }
    
    public class UpdateEntityCommand<TEntityForm> : Command
    {
        [FromBody]
        public long Id { get; set; }
    
        [FromBody]
        public TEntityForm Form { get; set; }
    }
    
    public class DeleteEntityCommand<TEntityForm> : Command
    {
        [FromBody]
        public long Id { get; set; }
    }
    
    public interface IEntityForm
    {
    
    }
}