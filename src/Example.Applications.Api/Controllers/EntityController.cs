using System;
using System.Linq;
using System.Threading.Tasks;
using Example.Applications.Domain;
using Foundation.CustomForm;
using Foundation.CustomForm.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Example.Applications.Api.Controllers
{
    public class EntityController : Controller
    {
        private readonly ICustomFormProvider _customFormProvider;
        private readonly ExampleDbContext _dbContext;


        public EntityController(ICustomFormProvider customFormProvider,  ExampleDbContext dbContext)
        {
            _customFormProvider = customFormProvider;
            _dbContext = dbContext;
        }
        
        [HttpGet("api/entity/{name}/{id}")]
        public Task<IActionResult> Get(string name,int id)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost("api/entity/{name}")]
        public async Task<IActionResult> Get(string name,[FromBody]JToken jsonBody)
        {
//            var customForm =  await _dbContext.Customers.SingleAsync(x=> x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            
            var metadata = _customFormProvider.FindCustomFormMetadata(typeof(Customer).Assembly).Single(x =>
                x.TypeMetadata.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
             
            var entityType = Type.GetType(metadata.TypeMetadata.AssemblyQualifiedName);
            var entity = jsonBody?.ToObject(entityType);
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
    
    
}