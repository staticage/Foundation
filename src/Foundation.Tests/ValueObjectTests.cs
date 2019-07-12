using System;
using System.Collections.Generic;
using FluentAssertions;
using Foundation.Core;
using Foundation.DDD;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests
{
    public class ValueObjectTests
    {
        public class Name : ValueObject
        {
            public Name(string firstName,string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }
            public string FirstName { get;  }
            public string LastName { get; }
        }
        
        public class FakeName : ValueObject
        {
            public FakeName(string firstName,string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }
            public string FirstName { get;  }
            public string LastName { get; }
        }
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_return_true_when_property_values_are_equal()
        {
            var name1 = new Name(firstName: "Alex",lastName: "Liu");
            var name2 = new Name(firstName: "Alex",lastName: "Liu");
            var name3 = new Name(firstName: "Alex",lastName: "Wang");
            name1.Equals(name2).Should().BeTrue();
            (name1 == name2).Should().BeTrue();
            (name1 != name2).Should().BeFalse();
            
            (name1 == name3).Should().BeFalse();
            (name1 != name3).Should().BeTrue();
            ReferenceEquals(name1, name2).Should().BeFalse();
        }
        
        [Test]
        public void Should_supports_json_serialization()
        {
            var name1 = new Name(firstName: "Alex",lastName: "Liu");
            var deserialized = JsonConvert.DeserializeObject<Name>(JsonConvert.SerializeObject(name1));

            name1.Equals(deserialized).Should().BeTrue();
        }
        
        [Test]
        public void Should_return_false_when_the_type_is_different()
        {
            var name = new Name(firstName: "Alex",lastName: "Liu");
            var fakedName = new FakeName(firstName: "Alex",lastName: "Liu");

            name.Equals(fakedName).Should().BeFalse();
        }
        
        [Test]
        public void TypeNameTests()
        {
            var type = typeof(string);
            Console.WriteLine(type.GetDisplayName());
            Console.WriteLine(type.Name);
            Console.WriteLine(type.FullName);
            Console.WriteLine(type.AssemblyQualifiedName);
            
            type = typeof(DateTime?);
            
            Console.WriteLine(type.GetDisplayName());
            Console.WriteLine(type.Name);
            Console.WriteLine(type.FullName);
            Console.WriteLine(type.AssemblyQualifiedName);
            
            type = typeof(string[]);
            
            Console.WriteLine(type.GetDisplayName());
            Console.WriteLine(type.Name);
            Console.WriteLine(type.FullName);
            Console.WriteLine(type.AssemblyQualifiedName);
            
            
            type = typeof(string[,,,]);
            
            Console.WriteLine(type.GetDisplayName());
            Console.WriteLine(type.Name);
            Console.WriteLine(type.FullName);
            Console.WriteLine(type.AssemblyQualifiedName);
            
            
            type = typeof(Dictionary<string[,,,],int[,,,]>);
            
            Console.WriteLine(type.GetDisplayName());
            Console.WriteLine(type.Name);
            Console.WriteLine(type.FullName);
            Console.WriteLine(type.AssemblyQualifiedName);
        }
    }
}