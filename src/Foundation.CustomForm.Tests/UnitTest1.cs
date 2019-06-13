using Foundation.CustomForm;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddCustomForm(cfg => cfg.UseNpgsql("Server=127.0.0.1;Port=5432;Database=custom_form_tests;User Id=postgres;Password=postgres;"));
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}