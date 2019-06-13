using Microsoft.EntityFrameworkCore;

namespace Foundation.CustomForm
{
    public class CustomFormDbContext : DbContext
    {
        public CustomFormDbContext(DbContextOptions<CustomFormDbContext> options) :base(options)
        {
        }
    }
}