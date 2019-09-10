using System;
using FluentAssertions;
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
            services.AddCustomForm(cfg => cfg.UseNpgsql("Server=127.0.0.1;Port=5432;Database=custom_form_tests;User Id=postgres;Password=sasa;"));
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void ServiceProviderScopeTest()
        {
            var services = new ServiceCollection();
            services.AddScoped<IFoo, FooA>();
            services.AddSingleton<SingleObject>();
            services.AddSingleton<SingleObject2>();

            var serviceProvider = services.BuildServiceProvider();
            var single1 = serviceProvider.GetService<SingleObject2>();
            Console.WriteLine(single1.ServiceProvider.GetHashCode());
            
            using (var scope = serviceProvider.CreateScope())
            {
                var foo = scope.ServiceProvider.GetService<IFoo>();
                foo.Foo().Should().NotBe(serviceProvider);

                var single2 = scope.ServiceProvider.GetService<SingleObject>();
                
                single2.ServiceProvider.Should().Be(single1.ServiceProvider);
                foo.Foo().Should().NotBe(single2.ServiceProvider);
                foo.Foo().Should().NotBe(single1.ServiceProvider);
            }
        }

        [Test]
        public void ServiceProviderTests()
        {
            
        }


        public class SingleObject
        {
            public IServiceProvider ServiceProvider { get; }

            public SingleObject(IServiceProvider serviceProvider)
            {
                ServiceProvider = serviceProvider;
            }
        }
        
        public class SingleObject2
        {
            public IServiceProvider ServiceProvider { get; }

            public SingleObject2(IServiceProvider serviceProvider)
            {
                ServiceProvider = serviceProvider;
            }
        }
        
        public interface IFoo
        {
            IServiceProvider Foo();
        }
        
        public class FooA : IFoo
        {
            private readonly IServiceProvider _serviceProvider;

            public FooA(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
                Console.WriteLine(serviceProvider.GetHashCode());
            }

            public IServiceProvider Foo()
            {
                return _serviceProvider;
            }
        }
    }
}