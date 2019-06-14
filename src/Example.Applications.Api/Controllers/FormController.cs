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
        
        [HttpGet("api/custom-form")]
        public  async Task<IActionResult> Get([FromQuery]string type)
        {
            return Ok(_dbContext.CustomForms.Where(x => type.IsNullOrEmpty() || x.Type == type).ToList());
        }

        [HttpPost("api/custom-form")]
        public async Task<IActionResult> Post([FromBody] CustomForm model)
        {
            await _dbContext.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
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