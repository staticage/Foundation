using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Example.Applications.Domain;
using Foundation.Core;
using Foundation.CustomForm;
using Foundation.CustomForm.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Example.Applications.Api.Controllers
{
    public class FormController : Controller
    {
        private readonly CustomFormDbContext _dbContext;
        private readonly ICustomFormProvider _customFormProvider;

        public FormController(CustomFormDbContext dbContext, ICustomFormProvider customFormProvider)
        {
            _dbContext = dbContext;
            _customFormProvider = customFormProvider;
        }
        
        [HttpGet("api/custom-form/_metadata")]
        public IActionResult Get()
        {
            return Ok(_customFormProvider.FindCustomFormMetadata(typeof(Customer).Assembly));
        }

        [HttpPost("api/custom-form/_query")]
        public async Task<IActionResult> Query([FromBody]CustomFormQuery query)
        {
            return Ok(await _dbContext.CustomForms.Where(x=> x.Type == query.Type).ToListAsync());
        }

        [HttpGet("api/custom-form/{id}")]
        public  async Task<IActionResult> Get([FromQuery]int id)
        {
            return Ok(await _dbContext.CustomForms.SingleAsync(x=> x.Id == id));
        }

        [HttpPost("api/custom-form")]
        public async Task<IActionResult> Post([FromBody] CustomForm model)
        {
            await _dbContext.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("api/custom-form")]
        public async Task<IActionResult> Put([FromBody] CustomForm model)
        {
            var customForm = await _dbContext.CustomForms.SingleAsync(x=> x.Id == model.Id);
            model.Populate(customForm);
            await _dbContext.SaveChangesAsync();
            return Ok(customForm);
        }
    }

    public class CustomFormQuery
    {
        public string Type { get;  set; }
    }

    public class SelectListController : Controller
    {
        IDictionary<string,Type> _enumTypeMap ;
        public SelectListController()
        {
            _enumTypeMap = new[]
            {
                typeof(FieldType)
            }.ToImmutableDictionary(x => x.Name, x => x, StringComparer.OrdinalIgnoreCase);
        }
        
        [HttpGet("api/select-list/{enumName}")]
        public object SelectList(string enumName)
        {
            if (!_enumTypeMap.ContainsKey(enumName))
            {
                return NotFound();
            }

            var enumType = _enumTypeMap[enumName];

            return Ok(Enum.GetNames(enumType).Select(x => new
            {
                label = enumType.GetField(x).GetCustomAttribute<DescriptionAttribute>()?.Description ?? x,
                value = x
            }).ToList());
        }
    }
}