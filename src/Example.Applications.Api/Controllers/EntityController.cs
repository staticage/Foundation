// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Linq;
// using System.Threading.Tasks;
//
// using Example.Applications.Domain;
// using Foundation.Core;
// using Foundation.CustomForm;
// using Foundation.CustomForm.Services;
//
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
//
// namespace Example.Applications.Api.Controllers
// {
//     public class EntityController : Controller
//     {
//         private readonly ICustomFormProvider _customFormProvider;
//         private readonly ExampleDbContext _dbContext;
//         
//         public EntityController(ICustomFormProvider customFormProvider, ExampleDbContext dbContext)
//         {
//             _customFormProvider = customFormProvider;
//             _dbContext = dbContext;
//         }
//
//         [HttpGet("api/entity/{name}/{id}")]
//         public async Task<IActionResult> Get(string name, int id)
//         {
//             var metadata = _customFormProvider.FindCustomFormMetadata(typeof(Customer).Assembly).Single(x =>
//                 x.TypeMetadata.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
//             var entityType = Type.GetType(metadata.TypeMetadata.AssemblyQualifiedName);
//             return Ok(await _dbContext.FindAsync(entityType, id));
//         }
//
//         // [HttpPost("api/entity/{name}/_query")]
//         // public async Task<IActionResult> Query(string name, [FromBody] JToken jsonBody)
//         // {
//         //     var metadata = _customFormProvider.FindCustomFormMetadata(typeof(Customer).Assembly).Single(x =>
//         //         x.TypeMetadata.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
//         //     var customForm = _customFormProvider.GetCustomFormSetting(name);
//         //     var entityType = Type.GetType(metadata.TypeMetadata.AssemblyQualifiedName);
//         //     var relational = _dbContext.Model.FindEntityType(entityType).Relational();
//         //     var sql = $"SELECT * FROM \"{relational.TableName}\" WHERE 1=1";
//         //     var parameters = jsonBody.ToObject<Dictionary<string, object>>();
//         //     
//         //     foreach (var key in parameters.Keys.ToList())
//         //     {
//         //         var field = customForm.Fields.Single(x => x.Name == key);
//         //         var property = entityType.GetProperty(key);
//         //         if (parameters[key]?.ToString().IsNullOrEmpty() ?? true)
//         //         {
//         //             continue;
//         //         }
//         //
//         //         if (parameters[key].GetType() != property)
//         //         {
//         //             parameters[key] = Convert.ChangeType(parameters[key], property.PropertyType);
//         //         }
//         //         
//         //         if (field.QueryOptions?.Type == QueryType.Equals)
//         //         {
//         //             sql += $" AND \"{key}\" = @{key}";
//         //         }
//         //         else if (field.QueryOptions?.Type == QueryType.Like)
//         //         {
//         //             sql += $" AND \"{key}\" like @{key}";
//         //             parameters[key] = $"%{parameters[key]}%";
//         //         }
//         //         
//         //         
//         //     }
//         //
//         //     var connection = _dbContext.Database.GetDbConnection();
//         //     if (connection.State != ConnectionState.Open)
//         //     {
//         //         connection.Open();
//         //     }
//         //     var dynamic = new DynamicParameters();
//         //     dynamic.AddDynamicParams(parameters);
//         //     
//         //     try
//         //     {
//         //         var result = await connection.QueryAsync(sql, dynamic);
//         //         return Ok(result);
//         //     }
//         //     catch (Exception e)
//         //     {
//         //         Console.WriteLine(e);
//         //         throw;
//         //     }
//         // }
//
//         [HttpPost("api/entity/{name}")]
//         public async Task<IActionResult> Post(string name, [FromBody] JToken jsonBody)
//         {
// //            var customForm =  await _dbContext.Customers.SingleAsync(x=> x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
//             try
//             {
//                 var metadata = _customFormProvider.FindCustomFormMetadata(typeof(Customer).Assembly).Single(x =>
//                     x.TypeMetadata.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
//
//                 var entityType = Type.GetType(metadata.TypeMetadata.AssemblyQualifiedName);
//                 JsonConvert.DefaultSettings = () => new JsonSerializerSettings
//                 {
//                     NullValueHandling = NullValueHandling.Ignore
//                 }; 
//                     
//                 var entity = jsonBody?.ToObject(entityType);
//                 await _dbContext.AddAsync(entity);
//                 await _dbContext.SaveChangesAsync();
//                 return Ok();
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine(e);
//                 throw;
//             }
//         }
//         
//         [HttpPut("api/entity/{name}")]
//         public async Task<IActionResult> Put(string name, [FromBody] JToken jsonBody)
//         {
//             try
//             {
//                 var metadata = _customFormProvider.FindCustomFormMetadata(typeof(Customer).Assembly).Single(x =>
//                     x.TypeMetadata.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
//
//                 var entityType = Type.GetType(metadata.TypeMetadata.AssemblyQualifiedName);
//                 
//                 var model = jsonBody?.ToObject(entityType);
//                 var entity = (object)_dbContext.Find(entityType, ((dynamic) model).Id);
//                 foreach (var property in entityType.GetProperties())
//                 {
//                     if (!(property.GetValue(model, null)?.Equals(property.GetValue(entity, null)) ?? true))
//                     {
//                         property.SetValue(entity, property.GetValue(model, null));
//                     }
//                 }
//                 
//                 await _dbContext.SaveChangesAsync();
//                 return Ok();
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine(e);
//                 throw;
//             }
//         }
//     }
// }