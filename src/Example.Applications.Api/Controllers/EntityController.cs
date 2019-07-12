using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Example.Applications.Domain;
using Foundation.CustomForm;
using Foundation.CustomForm.Services;
using Foundation.Data;
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
        
        [HttpPost("api/entity/{name}/_query")]
        public async Task<IActionResult> Query(string name,[FromBody]JToken jsonBody)
        {
            var metadata = _customFormProvider.FindCustomFormMetadata(typeof(Customer).Assembly).Single(x =>
                x.TypeMetadata.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            var entityType = Type.GetType(metadata.TypeMetadata.AssemblyQualifiedName);
            var relational = _dbContext.Model.FindEntityType(entityType).Relational();
            var sql = $"SELECT * FROM \"{relational.TableName}\" WHERE 1=1";
            var parameters = jsonBody.ToObject<Dictionary<string, string>>();
            foreach (var parameter in parameters)
            {
                sql += $" AND {parameter.Key}=@{parameter.Key}";
            }

            var connection = _dbContext.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            try
            {
       
                var result = await connection.QueryAsync(sql, new DynamicParameters(parameters));
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
        [HttpPost("api/entity/{name}")]
        public async Task<IActionResult> Get(string name,[FromBody]JToken jsonBody)
        {
//            var customForm =  await _dbContext.Customers.SingleAsync(x=> x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            try
            {
                var metadata = _customFormProvider.FindCustomFormMetadata(typeof(Customer).Assembly).Single(x =>
                    x.TypeMetadata.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
             
                var entityType = Type.GetType(metadata.TypeMetadata.AssemblyQualifiedName);
                var entity = jsonBody?.ToObject(entityType);
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
    
    
}