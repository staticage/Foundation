using System;

namespace Foundation.CustomForm.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomFormAttribute : Attribute
    {
        public string Name { get; set; }
        public CustomFormAttribute(string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class FormFieldAttribute : Attribute
    {
        public string Name { get; set; }
        public FormFieldAttribute(string name)
        {
            Name = name;
        }
    }
}