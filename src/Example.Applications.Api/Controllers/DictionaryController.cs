using System.Linq;
using System.Threading.Tasks;
using Foundation.Core;
using Foundation.CustomForm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Example.Applications.Api.Controllers
{
    public class SettingController : Controller
    {
        private readonly CustomFormDbContext _dbContext;

        public SettingController(CustomFormDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("api/setting/_query")]
        public async Task<IActionResult> Query([FromBody]CustomFormQuery query)
        {
            return Ok(await _dbContext.Settings.Where(x=> query.Name.IsNullOrEmpty() || x.Name == query.Name).OrderByDescending(x=> x.Id).ToListAsync());
        }

        [HttpGet("api/setting/{id}")]
        public  async Task<IActionResult> Get(int id)
        {
            return Ok(await _dbContext.Settings.SingleAsync(x=> x.Id == id));
        }

        [HttpPost("api/setting")]
        public async Task<IActionResult> Post([FromBody] Setting model)
        {
            await _dbContext.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("api/setting")]
        public async Task<IActionResult> Put([FromBody] Setting model)
        {
            var setting = await _dbContext.Settings.SingleAsync(x=> x.Id == model.Id);
            setting.Items = model.Items;
            setting.Name = model.Name;
            await _dbContext.SaveChangesAsync();
            return Ok(setting);
        }
    }
}